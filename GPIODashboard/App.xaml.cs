using System;
using GPIODashboard.Helpers;
using GPIODashboard.Models;
using GPIODashboard.Services;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace GPIODashboard
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;
		StationEnvironment m_Environment;

        SettingsToStorage m_SettingsToStorage;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public SettingsToStorage SettingsToStorage
        {
            get { return m_SettingsToStorage; }
        }
        public StationEnvironment Environment
        {
            get { return m_Environment; }
        }
        public App()
        {
            InitializeComponent();

            m_Environment = new StationEnvironment();
            m_Environment.getDataAsync();

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
            m_SettingsToStorage = new SettingsToStorage(this);
            this.Suspending += App_Suspending;
        }

        private void App_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();


            if (m_Environment != null)
            {
                m_SettingsToStorage.writeDatatoLocalStorage(); // alle Daten Speichern
                //		m_OpenCVEnvironment->getDataFaceReaderWriter()->writeDataFaces();
                //		m_OpenCVEnvironment->getDataFaces()->deleteDataFaces();



            }




            deferral.Complete();
        }

        ~App()
        {
            if (m_Environment != null)
            {
                //	m_OnVifCameraViewModel->writeDatatoLocalStorage();
    

            }

         }
        private  void InitEnvironment()
        {

            m_SettingsToStorage.readDatafromLocalStorage();
            
            // Callback and Eventing
            // Create a AppShell to act as the navigation context and navigate to the first page





        }
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            InitEnvironment();
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.MainPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage(this);
        }
    }
}
