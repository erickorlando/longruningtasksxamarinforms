namespace MyGPSLogic.DataTransferObjects
{
    // Clase de respuesta para saber el periodo de latencia.
    public class LatencyResponse : BaseResponse
    {
        public long NumberOfSeconds { get; set; }
    }
}