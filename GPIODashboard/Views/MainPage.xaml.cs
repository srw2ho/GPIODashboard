using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;

using GPIODashboard.Models;
using System.Collections.ObjectModel;
using Windows.Foundation.Collections;

using GPIOServiceConnector;
using Windows.Foundation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using Windows.UI.Popups;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace GPIODashboard.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {

        GPIOOBank m_OutPuts;
        GPIOOBank m_Inputs;
        PropertySet m_OutPutServiceConnectorConfig;
        PropertySet m_InputServiceConnectorConfig;
        GPIOServiceConnector.GPIOConnector m_GPIOConnector;
     //   IList<GPIOOBank> m_Banks;
        GPIOOInOutBanks m_Banks;
        private StationEnvironment m_Environment;

        public MainPage()
        {
            InitializeComponent();
            /*

            m_Banks = new List<GPIOOBank>();

            m_OutPuts = new GPIOOBank("Outputs");


            m_Inputs = new GPIOOBank("Inputs");
            //   ObservableCollection<GPIOObjects>m_GPIOOutputs = new ObservableCollection<GPIOObjects>();


            GPIOObjects m_GPIOOutPut5V = new GPIOObjects("GPIOOutPut5V");

            m_GPIOOutPut5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 17, 0, 0));
            m_GPIOOutPut5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 27, 0, 0));
            m_GPIOOutPut5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 23, 0, 0));
            m_GPIOOutPut5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 22, 0, 0));

            GPIOObjects m_GPIOOutPut3V3 = new GPIOObjects("GPIOOutPut3V3");

            m_GPIOOutPut3V3.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 13, 0, 0));
            m_GPIOOutPut3V3.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 19, 0, 0));
            m_GPIOOutPut3V3.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 11, 0, 0));
            m_GPIOOutPut3V3.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 21, 0, 0));

            GPIOObjects m_GPIOOutPutOC = new GPIOObjects("GPIOOutPutOpenCollector ");

            m_GPIOOutPutOC.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 4, 0, 0));
            m_GPIOOutPutOC.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 10, 0, 0));
            m_GPIOOutPutOC.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 9, 0, 0));
            m_GPIOOutPutOC.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 16, 0, 0));

            m_OutPuts.GPIOBanks.Add(m_GPIOOutPut5V);

            m_OutPuts.GPIOBanks.Add(m_GPIOOutPut3V3);

            m_OutPuts.GPIOBanks.Add(m_GPIOOutPutOC);

            GPIOObjects GPIOInputs5V = new GPIOObjects("4 GPIOInputs");

            GPIOInputs5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 15, 0, 0));
            GPIOInputs5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 14, 0, 0));
            GPIOInputs5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 12, 0, 0));
            GPIOInputs5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 20, 0, 0));

            GPIOObjects GPIOInputs5V8 = new GPIOObjects("4 GPIOInputs");

            GPIOInputs5V8.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 26, 0, 0));
            GPIOInputs5V8.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 25, 0, 0));
            GPIOInputs5V8.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 24, 0, 0));
            GPIOInputs5V8.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 5, 0, 0));

            GPIOObjects GPIOInputs5V4 = new GPIOObjects("4 GPIOInputs");
            GPIOInputs5V4.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 6, 0, 0));
            GPIOInputs5V4.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 7, 0, 0));
            GPIOInputs5V4.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 8, 0, 0));
            GPIOInputs5V4.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 18, 0, 0));

            m_Inputs.GPIOBanks.Add(GPIOInputs5V);
            m_Inputs.GPIOBanks.Add(GPIOInputs5V8);
            m_Inputs.GPIOBanks.Add(GPIOInputs5V4);

            m_OutPutServiceConnectorConfig = new PropertySet();
            m_InputServiceConnectorConfig = new PropertySet();
            m_InputServiceConnectorConfig.Add("HostName", PropertyValue.CreateString("WilliRaspiPlus"));
            m_InputServiceConnectorConfig.Add("Port", PropertyValue.CreateInt32(3005));

            m_OutPuts.createPropertySet(m_InputServiceConnectorConfig);
            m_Inputs.createPropertySet(m_InputServiceConnectorConfig);

            m_Banks.Add(m_Inputs);
            m_Banks.Add(m_OutPuts);
            */
            //   m_GPIOConnector = new GPIOConnector();

            m_OutPutServiceConnectorConfig = null;
            m_InputServiceConnectorConfig = null;
            m_GPIOConnector = null;
            m_Banks = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GPIOOBank OutPuts
        {
            get { return m_OutPuts; }

        }
        public string VisibleConnectorName
        {
            get { return m_GPIOConnector.HostName; }

        }
        

        public GPIOOBank Inputs
        {
            get { return m_Inputs; }

        }

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



        private void UpdateOutputPropertySets(GPIOObject GPIOObj, IPropertySet Outputpropertys)
        {

            //OutPut.readImages();
  
            //  string keyPinValue = string.Format("GPIO.{0:00}", OutPut.PinNumber);
            string keyPinValue = GPIOObj.PinName;
            double dblValue  =0;
            Object Obj = null;
            if (Outputpropertys.TryGetValue(keyPinValue, out Obj))
            {
                if (Obj != null)
                {
                    dblValue = (double)Obj;
                 //   doInputPropertyUpdate = (dblValue != GPIOObj.PinValue);
                    GPIOObj.PinValue = dblValue;
                }
            }

        }

        private void UpdateInputPropertySets(GPIOObject GPIOObj, IPropertySet Inputpropertys)
        {

            //OutPut.readImages();
            //  string keyPinValue = string.Format("GPIO.{0:00}", OutPut.PinNumber);
            string keyPinValue = GPIOObj.PinName;

            Object Valout;
            if (Inputpropertys.TryGetValue(keyPinValue, out Valout))
            {
                if (Valout != null)
                {
                    string nwLine = (string)GPIOObj.getPropertyLine();
                    Valout = nwLine;
                    m_InputServiceConnectorConfig[keyPinValue] = Valout;

                }

            }



        }
        private void UpdateState(IPropertySet Inputpropertys, int updateValue)
        {
            Object Valout;
            if (Inputpropertys.TryGetValue("UpdateState", out Valout))
            {
                if (Valout != null)
                {
                    int state = (int) Valout;
                    if (state!= updateValue)
                    {
                        Valout = (int)updateValue;
                        Inputpropertys["UpdateState"] = Valout;
                    }
                }

            }



        }

        private void ProcessPropertysFromGPIOConnector(IPropertySet propertys)
        {
            for (int i = 0; i < m_Banks.InOutBanks.Count; i++)
            {
                GPIOOBank bank = m_Banks.InOutBanks[i];

                foreach (GPIOObjects OutPuts in bank.GPIOBanks)
                {
                    foreach (GPIOObject GPIOObj in OutPuts.GPIOs)
                    {
                        UpdateOutputPropertySets(GPIOObj, propertys);

                    }

                }
            }


        }

   

        async private void GPIOConnector_ChangeGPIOs(object sender, IPropertySet args)
        {

     
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
            { // your code should be here

                ProcessPropertysFromGPIOConnector(args);
            });
           

            //        throw new NotImplementedException();
        }


    async private void GPIOConnector_startStreaming(object sender, Windows.Networking.Sockets.StreamSocket args)
    {
        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
        { // your code should be here

            startRecording.IsEnabled = false;
            stopRecording.IsEnabled = true;

        });
    }

    async private void GPIOConnector_stopStreaming(object sender, string args)
        {
         
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                { // your code should be here
                    startRecording.IsEnabled = true;
                    stopRecording.IsEnabled = false;
                    if (args.Length > 0)
                    {
                        var messageDialog = new MessageDialog(args);
                        await messageDialog.ShowAsync();
                    }


                });
            }


        }

        async private void StopConnector()
        {
            m_GPIOConnector.ChangeGPIOs -= GPIOConnector_ChangeGPIOs;
 

            await m_GPIOConnector.stopProcessingPackagesAsync();

            m_GPIOConnector.startStreaming -= GPIOConnector_startStreaming;
            m_GPIOConnector.stopStreaming -= GPIOConnector_stopStreaming;
            m_GPIOConnector.Failed -= GPIOConnector_stopStreaming;

        }

        async private void StartConnector()
        {
            m_GPIOConnector.ChangeGPIOs += GPIOConnector_ChangeGPIOs;
            m_GPIOConnector.startStreaming += GPIOConnector_startStreaming;
            m_GPIOConnector.stopStreaming += GPIOConnector_stopStreaming;
            m_GPIOConnector.Failed += GPIOConnector_stopStreaming;

            m_InputServiceConnectorConfig["HostName"] = m_Environment.HostName;
            m_InputServiceConnectorConfig["Port"] = m_Environment.Port;
           
            for (int i = 0; i < m_Banks.InOutBanks.Count; i++)
            {
                GPIOOBank bank = m_Banks.InOutBanks[i];

                foreach (GPIOObjects OutPuts in bank.GPIOBanks)
                {
                    foreach (GPIOObject GPIOObj in OutPuts.GPIOs)
                    {
                        UpdateInputPropertySets(GPIOObj, m_InputServiceConnectorConfig);

                    }

                }
            }
            UpdateState(m_InputServiceConnectorConfig, 1);
            await m_GPIOConnector.startProcessingPackagesAsync(m_InputServiceConnectorConfig, m_OutPutServiceConnectorConfig);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            StopConnector();
            base.OnNavigatingFrom(e);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is GPIODashboard.App)
            {

                GPIODashboard.App AppEnvironment = e.Parameter as GPIODashboard.App;

   
                if (AppEnvironment != null)
                {
                    m_Environment = AppEnvironment.Environment;
                    m_InputServiceConnectorConfig =m_Environment.InputServiceConnectorConfig;
                    m_OutPutServiceConnectorConfig = m_Environment.OutPutServiceConnectorConfig;
                    m_Banks = m_Environment.GPIOOInOutBanks;
                    m_GPIOConnector = m_Environment.Connector;
                    m_Inputs = m_Banks.InOutBanks[0];
                    m_OutPuts = m_Banks.InOutBanks[1];
                    m_Banks.readImages();
                    StartConnector();
                }
            }
            base.OnNavigatedTo(e);
        }

        private void ToggleSwitch_Loaded(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = sender as ToggleSwitch;
            toggle.Toggled += ToggleSwitch_Toggled;
            GPIOObject GPIOPin = getGPIOPinByName(toggle.Header.ToString());
            if (GPIOPin != null)
            {
                toggle.Tag = GPIOPin;
            }


        }

        private GPIOObject getGPIOPinByName(string Name)
        {

            for (int i = 0; i < m_Banks.InOutBanks.Count; i++)
            {
                GPIOOBank bank = m_Banks.InOutBanks[i];

                foreach (GPIOObjects OutPuts in bank.GPIOBanks)
                {
                    foreach (GPIOObject OutPut in OutPuts.GPIOs)
                    {
                        if (String.Compare (OutPut.PinName, Name) == 0)
                        {
                
                            return OutPut;
                        }

                    }

                }
            }



            return null;

        }

        private void ToggleSwitch_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
   
            ToggleSwitch tgl = sender as ToggleSwitch;
            if (tgl != null)
            {

                GPIOObject GPIOObj = tgl.Tag as GPIOObject;
                if (GPIOObj != null)
                {
                    GPIOObj.SetValue = tgl.IsOn? 1:0;
                    UpdateInputPropertySets(GPIOObj, m_InputServiceConnectorConfig);

                    /*
                    //        (GPIOObj.SetValue == 0) ? 1 : 0;
                    Object Valout = null;
                    if (this.m_InputServiceConnectorConfig.TryGetValue(GPIOObj.PinName, out Valout))
                    {
                        if (Valout != null)
                        {
                            string nwLine = (string)GPIOObj.getPropertyLine();
                            Valout = nwLine;
                            m_InputServiceConnectorConfig[GPIOObj.PinName] = Valout;

                        }

                    }
                    */

                }

            }

        }


        void stopRecording_Click(Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            StopConnector();
        }


        void startRecording_Click(Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            StartConnector();

        }
        void resetAllOutputs_Click(Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UpdateState(m_InputServiceConnectorConfig, 0);
            GPIOOBank bank = m_OutPuts;

            foreach (GPIOObjects OutPuts in bank.GPIOBanks)
            {
                foreach (GPIOObject OutPut in OutPuts.GPIOs)
                {
                    OutPut.SetValue = 0;
                    UpdateInputPropertySets(OutPut, m_InputServiceConnectorConfig);
                }

            }
            UpdateState(m_InputServiceConnectorConfig, 1);

        }
        


    }
}
