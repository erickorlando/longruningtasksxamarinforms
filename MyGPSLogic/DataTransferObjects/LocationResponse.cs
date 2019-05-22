namespace MyGPSLogic.DataTransferObjects
{
    public class LocationResponse : BaseResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
        public double? Speed { get; set; }

    }
}