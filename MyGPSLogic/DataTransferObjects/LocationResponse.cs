using System;

namespace MyGPSLogic.DataTransferObjects
{
    // Clase que permite obtener los datos recogidos por el GPS.
    public class LocationResponse : BaseResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
        public double? Speed { get; set; }
        public DateTime DateTime { get; set; }

        public LocationResponse()
        {
            DateTime = DateTime.Now;
        }

        public override string ToString()
        {
            return
                $"Latitud: {Latitude}, Longitud: {Longitude}, Altitud: {Altitude}, Velocidad: {Speed}, Hora: {DateTime:G}";
        }
    }
}