using System.Threading;
using MyGPSLogic.DataTransferObjects;

namespace MyGPSLogic.Services
{
    public interface IMyGps
    {
        GpsServiceResponse StartUp(CancellationToken cancellationToken);

        GpsServiceResponse ShutDown();

        LocationResponse GetGpsPositions();
    }
}