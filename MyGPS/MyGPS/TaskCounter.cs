using System;
using System.Threading;
using System.Threading.Tasks;
using MyGPSLogic.Messages;
using MyGPSLogic.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyGPS
{
    public class TaskCounter
    {
        private Timer _timer;
        private IDriver _driver;

        public TaskCounter(IDriver driver)
        {
            _driver = driver;
            _timer = new Timer(x => ElapsedTime(), driver, 0, driver.GetLatency().NumberOfSeconds);
        }

        private void ElapsedTime()
        {
                
        }

        public async Task RunCounter(CancellationToken token)
        {
            await Task.Run(async () =>
            {
                for (long i = 0; i < long.MaxValue; i++)
                {
                    token.ThrowIfCancellationRequested();

                    await Task.Delay(1000, token); //Demoramos a proposito 1 segundo.

                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var position = await Geolocation.GetLocationAsync(request, token);

                    if (position == null) continue;
                    
                    var message = new TickedMessage
                    {
                        Message =
                            $"Latitud: {position.Latitude}. Longitud: {position.Longitude}, Altitud: {position.Altitude}, Velocidad: {position.Speed} Hora: {DateTime.Now:T}"
                    };

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MessagingCenter.Send(message, nameof(TickedMessage));
                    });
                }
            }, token);
        }
    }
}