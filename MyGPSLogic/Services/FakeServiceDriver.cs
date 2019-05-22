using System;
using MyGPSLogic.DataTransferObjects;

namespace MyGPSLogic.Services
{
    public class FakeServiceDriver : IDriver
    {
        public LatencyResponse GetLatency()
        {
            var response = new LatencyResponse();
            try
            {
                // Llamada al API REST
                response.NumberOfSeconds = 60;
            }
            catch (Exception ex)
            {
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
                // Hacer la llamada al API REST.
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}