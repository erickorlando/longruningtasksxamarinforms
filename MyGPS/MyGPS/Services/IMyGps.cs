namespace MyGPS.Services
{
    public interface IMyGps
    {
        void StartUp();

        void ShutDown();

        void GetGpsPositions();

        void SendGpsPositions();

    }
}