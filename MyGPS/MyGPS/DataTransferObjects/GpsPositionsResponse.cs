using System.Collections.Generic;

namespace MyGPS.DataTransferObjects
{
    public class GpsPositionsResponse
    {
        public ICollection<LocationResponse> Locations { get; set; }
    }

    public class LocationResponse
    {
        public decimal? Altitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Speed { get; set; }

    }
}