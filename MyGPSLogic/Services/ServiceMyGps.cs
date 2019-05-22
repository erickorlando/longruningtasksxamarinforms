using MyGPSLogic.DataTransferObjects;
using MyGPSLogic.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyGPSLogic.Services
{
    public class ServiceMyGps : IMyGps
    {
        private readonly IDriver _driver;
        private Timer _timer;
        private long _lastLatency;
        private LocationResponse _locationResponse;

        public CancellationToken Token { get; private set; }

        public ServiceMyGps(IDriver driver)
        {
            _driver = driver;
        }

        public GpsServiceResponse StartUp(CancellationToken cancellationToken)
        {
            var response = new GpsServiceResponse();
            try
            {
                Token = cancellationToken;

                var message = new StartLongRunningTaskMessage();
                MessagingCenter.Send(message, nameof(StartLongRunningTaskMessage));

                // Obtenemos la latencia.
                var latency = _driver.GetLatency();
                if (!latency.Success)
                    throw new InvalidOperationException(latency.ErrorMessage);

                _lastLatency = latency.NumberOfSeconds;

                // Inicializamos el Timer con la cantidad de segundos por mil
                // ya que se tiene que expresar en milisegundos.

                _timer = new Timer(ElapsedTime, this, 0, _lastLatency * 1000);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public GpsServiceResponse ShutDown()
        {
            var response = new GpsServiceResponse();
            try
            {
                var message = new StartLongRunningTaskMessage();
                MessagingCenter.Send(message, nameof(StartLongRunningTaskMessage));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public LocationResponse GetGpsPositions()
        {
            return _locationResponse;
        }

        private void ElapsedTime(object state)
        {
            // Hacemos cast del objeto recibido.
            var stateRequest = (GpsPositionRequest)state;

            Task.Run(async () =>
            {
                stateRequest
                    .CancellationToken
                    .ThrowIfCancellationRequested();

                var response = new LocationResponse();

                try
                {
                    // En cada iteracion debemos preguntar por el latency.
                    var latency = _driver.GetLatency();
                    if (!latency.Success)
                        throw new InvalidOperationException(latency.ErrorMessage);

                    // Comprobamos que el valor del Latency no haya cambiado.
                    if (_lastLatency != latency.NumberOfSeconds)
                    {
                        _lastLatency = latency.NumberOfSeconds;
                        // Cambiamos el valor del Period del Timer.
                        _timer.Change(0, _lastLatency * 1000);
                        return; // Se sale del metodo.
                    }

                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var position = await Geolocation.GetLocationAsync(request, stateRequest.CancellationToken);

                    if (position == null)
                    {
                        response.Success = false;
                        response.ErrorMessage = "No se pudo obtener la locacion";
                    }
                    else
                    {
                        response.Altitude = position.Altitude;
                        response.Latitude = position.Latitude;
                        response.Longitude = position.Longitude;
                        response.Speed = position.Speed;
                        response.DateTime = DateTime.Now;

                        var sendPositionResponse = _driver.SendGpsPositions(response);

                        response.Success = sendPositionResponse.Success;
                        response.ErrorMessage = sendPositionResponse.ErrorMessage;
                    }
                }
                catch (FeatureNotSupportedException ex)
                {
                    response.Success = false;
                    response.ErrorMessage = ex.Message;
                }
                catch (FeatureNotEnabledException ex)
                {
                    response.Success = false;
                    response.ErrorMessage = ex.Message;
                }
                catch (PermissionException ex)
                {
                    response.Success = false;
                    response.ErrorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.ErrorMessage = ex.Message;
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    MessagingCenter.Send(response, nameof(LocationResponse));
                });

                // Almacenamos de manera local en memoria el valor de LocationResponse
                _locationResponse = response;

            }, stateRequest.CancellationToken);
        }

    }
}
