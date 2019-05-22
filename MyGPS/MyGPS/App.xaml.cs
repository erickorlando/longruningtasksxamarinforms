using System;
using MyGPSLogic.Messages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyGPS
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Ni bien se ejecute la aplicacion, se debe invocar el inicio del Servicio.
            var message = new StartLongRunningTaskMessage();
            MessagingCenter.Send(message, nameof(StartLongRunningTaskMessage));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
