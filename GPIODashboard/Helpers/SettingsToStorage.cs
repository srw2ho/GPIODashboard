
using GPIODashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GPIODashboard.Helpers
{
    public class SettingsToStorage
    {
        private LocalStorageSettings m_LocalStorageSettings;

        private LocalStorageItem m_localStorage;
        private StationEnvironment m_StationEnvironment;


        public SettingsToStorage(GPIODashboard.App app)
        {

            m_LocalStorageSettings = new LocalStorageSettings("GPIODashboardAppEnvironment");
   
            m_localStorage = new LocalStorageItem("GPIODashboardStation");
            m_StationEnvironment = app.Environment;



        }


        public bool writeStationEnvironmenttoLocalStorage(StationEnvironment StationEnvironment , Windows.Storage.ApplicationDataCompositeValue composite, int ListenerIdx)
        {

            if (m_localStorage == null) return false;
            m_localStorage.SetSourceIDName("StationEnvironment", ListenerIdx);

            int Idx = -1;

            bool bok = m_localStorage.writeSettingsToLocalStorage(composite, Idx);

            bok = m_localStorage.writeStringSettingsToLocalStorage(composite, m_localStorage.getCompositePropertyIDName("StationEnvironment.HostName", Idx), StationEnvironment.HostName);
            bok = m_localStorage.writeIntegerSettingsToLocalStorage(composite, m_localStorage.getCompositePropertyIDName("StationEnvironment.Port", Idx), StationEnvironment.Port);

            return bok;

        }

        public bool readStationEnvironmentDatafromLocalStorage(StationEnvironment StationEnvironment, Windows.Storage.ApplicationDataCompositeValue composite, int ListenerIdx)
        {

            if (m_localStorage == null) return false;
            m_localStorage.SetSourceIDName("StationEnvironment", ListenerIdx);


            int Idx = -1;
            bool bStoreOk = m_localStorage.readSettingsfromLocalStorage(composite, Idx);

            if (bStoreOk)
            {
                string StringValue;

                int IntValue;
  

                bool bok = m_localStorage.readStringSettingsfromLocalStorage(composite, m_localStorage.getCompositePropertyIDName("StationEnvironment.HostName", Idx), out StringValue);
                StationEnvironment.HostName = StringValue;


                bok = m_localStorage.readIntegerSettingsfromLocalStorage(composite, m_localStorage.getCompositePropertyIDName("StationEnvironment.Port", Idx), out IntValue);
                StationEnvironment.Port = IntValue;


            }
            else
            {
                createDummyConnection();
            }


            return bStoreOk;


        }



        protected void createDummyConnection()
        {

    
            m_StationEnvironment.HostName = "localhost";
            m_StationEnvironment.Port = 3005;

        }

        public bool writeListenerDatatoLocalStorage()
        {


            m_LocalStorageSettings.SetSourceIDName("GPIODashboardAppData");

            m_LocalStorageSettings.deleteCompositeValue(); // vor jedem Schreiben alles löschen



            Windows.Storage.ApplicationDataCompositeValue composite = m_LocalStorageSettings.getCompositeValue();

            int Idx = 0;
            writeStationEnvironmenttoLocalStorage(m_StationEnvironment, composite, Idx);

 
            m_LocalStorageSettings.writeCompositeValuetoLocalStorage();

            return true;
        }

        public bool readListenerDatafromLocalStorage()
        {


            m_LocalStorageSettings.SetSourceIDName("GPIODashboardAppData");

            Windows.Storage.ApplicationDataCompositeValue composite = m_LocalStorageSettings.getCompositeValue();
            int Idx = 0;

            bool bdata = readStationEnvironmentDatafromLocalStorage(m_StationEnvironment, composite, Idx);

            return bdata;
        }




        public bool writeDatatoLocalStorage()
        {


            writeListenerDatatoLocalStorage();


            return true;
        }

        public bool readDatafromLocalStorage()
        {

            readListenerDatafromLocalStorage();



            return true;
   
        }


    }
}
