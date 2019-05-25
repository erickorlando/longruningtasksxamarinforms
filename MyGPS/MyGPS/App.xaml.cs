using System;
using MyGPSLogic.Messages;
using MyGPSLogic.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyGPS
{
    public partial class App : Application
    {
        private static IMyGps _servicioGps;
        public static IMyGps ServicioGps => _servicioGps 
                                            ?? (_servicioGps = new ServiceMyGps(new FakeServiceDriver()));

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            
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
