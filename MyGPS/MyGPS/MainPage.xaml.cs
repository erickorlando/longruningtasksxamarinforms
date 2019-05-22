using System;
using System.ComponentModel;
using MyGPSLogic.DataTransferObjects;
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
            MessagingCenter.Subscribe<LocationResponse>(this, nameof(LocationResponse), message =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    LblPosicion.Text = message.ToString();
                });
            });
        }

        private void BtnDetener_OnClicked(object sender, EventArgs e)
        {
            var message = new StopLongRunningTaskMessage();
            MessagingCenter.Send(message, nameof(StopLongRunningTaskMessage));
        }
    }
}
