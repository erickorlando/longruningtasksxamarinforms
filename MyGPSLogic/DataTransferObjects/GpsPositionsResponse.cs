using System.Collections.Generic;

namespace MyGPSLogic.DataTransferObjects
{
    public class GpsPositionsResponse : BaseResponse
    {
        public ICollection<LocationResponse> Locations { get; set; }

        public GpsPositionsResponse()
        {
            Locations = new List<LocationResponse>();
        }
    }

}