using GPIODashboard.Helpers;
using GPIODashboard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GPIODashboard.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EnvironmentConfig : Page
    {
        private StationEnvironment m_Environment;
        SettingsToStorage m_SettingsToStorage;
        public EnvironmentConfig()
        {
            this.InitializeComponent();
            m_Environment = null;
            m_SettingsToStorage = null;
        }

        public StationEnvironment Environment
        {
            get { return m_Environment; }
            set { m_Environment = value; }
        }


        private void OnKeyUpHandler(object sender, KeyRoutedEventArgs e)
        {
            TextBox TeBox = sender as TextBox;
            if (TeBox != null)
            {

                if (e.Key == Windows.System.VirtualKey.Enter)
                {
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                    e.Handled = true;
                }
            }
            base.OnKeyUp(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is GPIODashboard.App)
            {

                GPIODashboard.App AppEnvironment = e.Parameter as GPIODashboard.App;

                if (AppEnvironment != null)
                {
                    m_Environment = AppEnvironment.Environment;

                    if (m_Environment != null)
                    {
                        m_SettingsToStorage = AppEnvironment.SettingsToStorage;

                    }

                }



            }

            base.OnNavigatedTo(e);
        }


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //       this.stopProcessing();

            if (m_SettingsToStorage != null)
            {
                m_SettingsToStorage.writeDatatoLocalStorage();
            }
            base.OnNavigatingFrom(e);

        }
    }

}



