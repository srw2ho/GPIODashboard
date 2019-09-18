using GPIOServiceConnector;
using System.ComponentModel;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace GPIODashboard.Models
{
    public class StationEnvironment : INotifyPropertyChanged
    {
        string m_HostName;
        int m_Port;
        public event PropertyChangedEventHandler PropertyChanged;
        GPIOOInOutBanks m_GPIOOInOutBanks;
        PropertySet m_OutPutServiceConnectorConfig;
        PropertySet m_InputServiceConnectorConfig;
        GPIOConnector m_GPIOConnector;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        public StationEnvironment()
        {
            m_HostName = "localhost";
            m_Port = 3005;
            m_GPIOConnector = new GPIOConnector();
            m_OutPutServiceConnectorConfig = new PropertySet();
            m_InputServiceConnectorConfig = new PropertySet();
            m_InputServiceConnectorConfig.Add("HostName", PropertyValue.CreateString("WilliRaspiPlus"));
            m_InputServiceConnectorConfig.Add("Port", PropertyValue.CreateInt32(3005));
            m_InputServiceConnectorConfig.Add("UpdateState", PropertyValue.CreateInt32(0));

            m_GPIOOInOutBanks = null;


        }

  

        public GPIOConnector Connector
        {
            get
            {
                return m_GPIOConnector;
            }

        }
        public GPIOOInOutBanks GPIOOInOutBanks
        {
            get
            {
                return m_GPIOOInOutBanks;
            }

        }

        public PropertySet OutPutServiceConnectorConfig
        {
            get
            {
                return m_OutPutServiceConnectorConfig;
            }

        }

        public PropertySet InputServiceConnectorConfig
        {
            get
            {
                return m_InputServiceConnectorConfig;
            }

        }

        async public void getDataAsync()
        {
             m_GPIOOInOutBanks = await GPIOOInOutBanks.GPIOOInOutBanksAsync(m_InputServiceConnectorConfig);

        }

        public string HostName
        {
            get
            {
                return m_HostName;
            }
            set
            {
                m_HostName = value;
                OnPropertyChanged("HostName");
            }

        }
        public int Port
        {
            get
            {
                return m_Port;
            }
            set
            {
                m_Port = value;
                OnPropertyChanged("Port");
            }

        }

    }
}
