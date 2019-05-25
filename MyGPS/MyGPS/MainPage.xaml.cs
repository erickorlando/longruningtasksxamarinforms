using MyGPSLogic.DataTransferObjects;
using MyGPSLogic.Messages;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Plugin.LocalNotifications;

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
            // Ni bien se abra el ContentPage se nos suscribimos a los mensajes.
            HandleReceivedMessages();
        }

        void HandleReceivedMessages()
        {
            // Respuesta de LocationResponse.
            MessagingCenter.Subscribe<LocationResponse>(this, nameof(LocationResponse), message =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    LblPosicion.Text = message.ToString();
                });
            });

            // Respuesta de CancelledMessage.
            MessagingCenter.Subscribe<CancelledMessage>(this, nameof(CancelledMessage), message =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    LblPosicion.Text = message.ToString();
                });
            });
        }


        private void BtnIniciar_OnClicked(object sender, EventArgs e)
        {
            // Enviamos un mensaje con la intencion de invocar el inicio del Servicio.
            var message = new StartLongRunningTaskMessage();
            MessagingCenter.Send(message, nameof(StartLongRunningTaskMessage));
        }

        private void BtnNotificacion_OnClicked(object sender, EventArgs e)
        {
            CrossLocalNotifications.Current.Show("MyGPS", "La aplicacion se encuentra en ejecucion");
        }

        private void BtnDetener_OnClicked(object sender, EventArgs e)
        {
            // Enviamos un mensaje con la intencion de dar fin al servicio.
            var message = new StopLongRunningTaskMessage();
            MessagingCenter.Send(message, nameof(StopLongRunningTaskMessage));
        }
    }
}
