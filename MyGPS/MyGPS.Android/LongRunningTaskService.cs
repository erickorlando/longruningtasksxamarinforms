using Android.App;
using Android.Content;
using Android.OS;
using MyGPSLogic.Messages;
using MyGPSLogic.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyGPS.Droid
{
    [Service]
    public class LongRunningTaskService : Service
    {
        private CancellationTokenSource _cts;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                try
                {
                    var myGpsService = new ServiceMyGps(new FakeServiceDriver());
                    myGpsService.StartUp(_cts.Token);
                }
                catch (Android.OS.OperationCanceledException ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    if (_cts.IsCancellationRequested)
                    {
                        var message = new CancelledMessage();
                        Device.BeginInvokeOnMainThread(
                            () => MessagingCenter.Send(message, nameof(CancelledMessage))
                        );
					}
                }
            }, _cts.Token);

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();

                _cts.Cancel();
            }
            base.OnDestroy();
        }
    }
}