using System;
using System.ComponentModel;
using MyGPSLogic.Messages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyGPS
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            HandleReceivedMessages();
        }

        void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<TickedMessage>(this, nameof(TickedMessage), message =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    LblPosicion.Text = message.Message;
                });
            });
        }

        private void BtnIniciar_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var message = new StartLongRunningTaskMessage();
                MessagingCenter.Send(message, nameof(StartLongRunningTaskMessage));
            }
            catch (FeatureNotSupportedException ex)
            {
                // Handle not supported on device exception
                LblPosicion.Text = ex.Message;
            }
            catch (FeatureNotEnabledException ex)
            {
                // Handle not enabled on device exception
                LblPosicion.Text = ex.Message;
            }
            catch (PermissionException ex)
            {
                // Handle permission exception
                LblPosicion.Text = ex.Message;
            }
            catch (Exception ex)
            {
                // Unable to get location
                LblPosicion.Text = ex.Message;
            }
        }

        private void BtnDetener_OnClicked(object sender, EventArgs e)
        {
            var message = new StopLongRunningTaskMessage();
            MessagingCenter.Send(message, nameof(StopLongRunningTaskMessage));
        }
    }
}
