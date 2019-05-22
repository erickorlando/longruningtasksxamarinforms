using System.Threading;

namespace MyGPSLogic.DataTransferObjects
{
    public class GpsPositionRequest
    {
        public long Latency { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}