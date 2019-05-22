using MyGPSLogic.DataTransferObjects;

namespace MyGPSLogic.Services
{
    public interface IDriver
    {
        LatencyResponse GetLatency();

        SentPositionsResponse SendGpsPositions(LocationResponse request);
    }
}