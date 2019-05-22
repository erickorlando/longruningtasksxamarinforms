using MyGPSLogic.DataTransferObjects;

namespace MyGPSLogic.Services
{
    public interface IMyGps
    {
        void StartUp();

        void ShutDown();

        LocationResponse GetGpsPositions(LatencyResponse request);
    }
}