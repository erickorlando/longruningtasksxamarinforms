using System;
using MyGPSLogic.DataTransferObjects;

namespace MyGPSLogic.Services
{
    /// <summary>
    /// Clase que simula el proxy con el API REST
    /// </summary>
    public class FakeServiceDriver : IDriver
    {
        public LatencyResponse GetLatency()
        {
            var response = new LatencyResponse();
            try
            {
                //TODO: Llamada al API REST
                response.NumberOfSeconds = 60;
            }
            catch (Exception ex)
            {
                // En caso falle, el parametro Success es false
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public SentPositionsResponse SendGpsPositions(LocationResponse request)
        {
            // Este metodo deberia enviar al API REST los datos recibidos por el objeto Request.
            var response = new SentPositionsResponse();
            try
            {
                //TODO: Hacer la llamada al API REST.
                response.Success = true;
            }
            catch (Exception ex)
            {
                // En caso falle, el parametro Success es false
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}