using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using GPIODashboard.Helpers;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.Core;

namespace GPIODashboard.Models
{
    public class GPIOObject : INotifyPropertyChanged
    {
        public enum GPIOTyp
        {
            input   = 0,
            output  = 1,
            PWM     = 2,
            HC_SR04 = 3,
            BME280  = 4,
            inputShutdown = 5,
            PWM9685     = 6,
        };

        //   enum GPIOTyp { Sun, Mon, Tue, Wed, Thu, Fri, Sat };

        string m_PinName;
        string m_GPIOName;
        GPIOTyp m_GPIOTyp;
        double m_PinValue;      // Value
        double m_InitValue;     // Initialisierungs Value
        double m_SetValue;      // für Output und PWM-Output
        int m_PinNumber;
        BitmapImage m_IsOnBitmapImage;
        BitmapImage m_IsOffBitmapImage;
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        public GPIOObject(string Name, GPIOTyp typ, int PinNo, double initValue, double SetValue)
        {
            //m_ScatterLineSeries = lineSeries;
            m_PinName =  string.Format("{0}.{1:00}", Name, PinNo); ;
            switch (typ)
            {
                case GPIOTyp.input:
                    m_GPIOName = string.Format("GPI.{0:00}", PinNo); ;
                    break;
                case GPIOTyp.output:
                    m_GPIOName = string.Format("GPO.{0:00}", PinNo); ;
                    break;
                default:
                    m_GPIOName = string.Format("GPIO.{0:00}", PinNo); ;
                    break;
            }


            m_GPIOTyp = typ;
            m_PinValue = 0;
            m_InitValue = initValue;
            m_SetValue = SetValue;
            m_PinNumber = PinNo;
            m_IsOnBitmapImage = null;
            m_IsOffBitmapImage = null;


      

        }
        public string getPropertyLine()
        {

            string keyValue = string.Format("PinName={0}; Typ={1}; PinNumber={2}; InitValue={3}; SetValue={4}; PinValue={5}", m_PinName, m_GPIOTyp.ToString(), m_PinNumber, m_InitValue, m_SetValue, m_PinValue); ;

            return keyValue;





        }

        public void createPropertySet(IPropertySet property)
        {
            string keyValue = getPropertyLine();

            property.Add(m_PinName, PropertyValue.CreateString(keyValue));

        //    string keyValue = string.Format("PinName={0}; Typ={1}; PinNumber={2}; InitValue={3}; SetValue={4}", m_GPIOName, m_GPIOTyp.ToString(), m_PinNumber, m_InitValue,m_SetValue-SetValue); ;

        //    property.Add(m_GPIOName, PropertyValue.CreateString(keyValue));




        }

        public void readImages()
        {

            if (m_GPIOTyp == GPIOTyp.input)
            {
                if (m_IsOnBitmapImage == null)
                {
                    m_IsOnBitmapImage = ImageHelper.ImageFromImagesFile("Signal_1.bmp");
                }
                if (m_IsOffBitmapImage == null)
                {
                    m_IsOffBitmapImage = ImageHelper.ImageFromImagesFile("Signal_0.bmp");
                }
            }




        }

        public BitmapImage IsOnBitmapImage
        {
            get
            {
       
           //     m_IsOnBitmapImage = ImageHelper.ImageFromImagesFile("Signal_1.bmp");
           //     m_IsOffBitmapImage = ImageHelper.ImageFromImagesFile("Signal_0.bmp");

                return (m_PinValue > 0) ? m_IsOnBitmapImage : m_IsOffBitmapImage;

            }

        }

        public string PinValueasString
        {
            get
            {
                string body = string.Format(" {0:00.00}", m_PinValue);
                return body;
            }

        }

        public string GPIOName
        {
            get
            {
                return m_GPIOName;
            }
            set
            {
                m_GPIOName = value;
                OnPropertyChanged("GPIOName");
            }

        }

        public string PinName
        {
            get
            {
                return m_PinName;
            }
            set
            {
                m_PinName = value;
                OnPropertyChanged("PinName");
            }

        }

        public GPIOTyp GPIOtyp
        {
            get { return m_GPIOTyp; }
            set
            {
                m_GPIOTyp = value;
                OnPropertyChanged("GPIOtyp");
            }

        }



        public double SetValue
        {
            get { return m_SetValue; }
            set
            {
                if (value != m_SetValue)
                {
                    m_SetValue = value;
                    OnPropertyChanged("SetValue");
                }

            }

        }

        public int PinNumber
        {
            get { return m_PinNumber; }
            set
            {
                m_PinNumber = value;
                OnPropertyChanged("PinNumber");
            }

        }
        public bool IsOn
        {
            get {
                return m_PinValue > 0;
            }
            set
            {
                m_PinValue = (value) ? 1:0;
                OnPropertyChanged("IsOn");
            }

        }

        public double PinValue
        {
            get { return m_PinValue; }
            set
            {
                if (value!= m_PinValue)
                {
                    m_PinValue = value;
                    OnPropertyChanged("PinValue");
                    OnPropertyChanged("PinValueasString");
                    OnPropertyChanged("IsOn");
                    OnPropertyChanged("IsOnBitmapImage");
                    
                }

            }

        }

        public double InitValue
        {
            get { return m_InitValue; }
            set
            {
                m_InitValue = value;
                OnPropertyChanged("InitValue");
            }

        }

    }

    public class GPIOObjects
    {
        ObservableCollection<GPIOObject> m_GPIOs;
        string m_BankName;

        public GPIOObjects(string Name)
        {
            m_BankName = Name;
            m_GPIOs = new ObservableCollection<GPIOObject>();
          

        }
        public void createPropertySet(IPropertySet property)
        {
            for (int i = 0; i < m_GPIOs.Count; i++)
            {
                GPIOObject obj = m_GPIOs[i];
                obj.createPropertySet(property);

            }

        }

        public void readImages()
        {
            for (int i = 0; i < m_GPIOs.Count; i++)
            {
                GPIOObject obj = m_GPIOs[i];
                obj.readImages();

            }

        }


        public GPIOObject getGPIOByName(string name)
        {

            for (int i = 0; i < m_GPIOs.Count; i++)
            {
                GPIOObject obj = m_GPIOs[i];
                if (obj.PinName == name)
                {
                    return obj;
                }

            }
            return null;

        }

        public ObservableCollection<GPIOObject> GPIOs
        {
            get
            {
                return m_GPIOs;
            }
        }

        public string BankName
        {
            get
            {
                return m_BankName;
            }
            set
            {
                m_BankName = value;
            }

        }

    }
    public class GPIOOBank
    {
        ObservableCollection<GPIOObjects> m_GPIOBanks;
        string m_BankName;

        public GPIOOBank(string Name)
        {
            m_BankName = Name;
            m_GPIOBanks = new ObservableCollection<GPIOObjects>();


        }

        public void createPropertySet(IPropertySet property)
        {
            for (int i = 0; i < m_GPIOBanks.Count; i++)
            {
                GPIOObjects obj = m_GPIOBanks[i];
                obj.createPropertySet(property);

            }

        }
        public void readImages()
        {
            for (int i = 0; i < m_GPIOBanks.Count; i++)
            {
                GPIOObjects obj = m_GPIOBanks[i];
                obj.readImages();

            }

        }

        public GPIOObjects getGPIOBankByName(string name)
        {

            for (int i = 0; i < m_GPIOBanks.Count; i++)
            {
                GPIOObjects obj = m_GPIOBanks[i];
                if (obj.BankName == name)
                {
                    return obj;
                }

            }
            return null;

        }

        public ObservableCollection<GPIOObjects> GPIOBanks
        {
            get
            {
                return m_GPIOBanks;
            }
        }

        public string BankName
        {
            get
            {
                return m_BankName;
            }
            set
            {
                m_BankName = value;
            }

        }

    }

    public class GPIOOInOutBanks
    {
        ObservableCollection<GPIOOBank> m_InOutBanks;
   
        string m_BankName;

        public GPIOOInOutBanks(string Name)
        {
            m_BankName = Name;
            m_InOutBanks = new ObservableCollection<GPIOOBank>();


        }
    
        static GPIOOInOutBanks Allocate(IPropertySet property)
        {
            GPIOOInOutBanks m_GPIOInOutBanks = new GPIOOInOutBanks("");

            //   m_Banks = new List<GPIOOBank>();

            GPIOOBank m_OutPuts = new GPIOOBank("Outputs");


            GPIOOBank m_Inputs = new GPIOOBank("Inputs");
            //   ObservableCollection<GPIOObjects>m_GPIOOutputs = new ObservableCollection<GPIOObjects>();


            GPIOObjects m_GPIOOutPut5V = new GPIOObjects("GPIOOutPut.5V");

            m_GPIOOutPut5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 17, 0, 0));
            m_GPIOOutPut5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 27, 0, 0));
            m_GPIOOutPut5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 23, 0, 0));
            m_GPIOOutPut5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 22, 0, 0));

            GPIOObjects m_GPIOOutPut3V3 = new GPIOObjects("GPIOOutPut.3V3");

            m_GPIOOutPut3V3.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 13, 0, 0));
            m_GPIOOutPut3V3.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 19, 0, 0));
            m_GPIOOutPut3V3.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 11, 0, 0));
            m_GPIOOutPut3V3.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 21, 0, 0));

            GPIOObjects m_GPIOOutPutOC = new GPIOObjects("GPIOOutPut.OpenCollector ");

            m_GPIOOutPutOC.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 4, 0, 0));
            m_GPIOOutPutOC.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 10, 0, 0));
            m_GPIOOutPutOC.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 9, 0, 0));
            m_GPIOOutPutOC.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.output, 16, 0, 0));

            m_OutPuts.GPIOBanks.Add(m_GPIOOutPut5V);

            m_OutPuts.GPIOBanks.Add(m_GPIOOutPut3V3);

            m_OutPuts.GPIOBanks.Add(m_GPIOOutPutOC);

            GPIOObjects GPIOInputs5V = new GPIOObjects("GPIOInputs.4Bank");

            GPIOInputs5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 15, 0, 0));
            GPIOInputs5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 14, 0, 0));
            GPIOInputs5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 12, 0, 0));
            GPIOInputs5V.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 20, 0, 0));

            GPIOObjects GPIOInputs5V8 = new GPIOObjects("GPIOInputs.8Bank");

            GPIOInputs5V8.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 26, 0, 0));
            GPIOInputs5V8.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 25, 0, 0));
            GPIOInputs5V8.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 24, 0, 0));
            GPIOInputs5V8.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 5, 0, 0));

            GPIOObjects GPIOInputs5V4 = new GPIOObjects("GPIOInputs.8Bank");
            GPIOInputs5V4.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 6, 0, 0));
            GPIOInputs5V4.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 7, 0, 0));
            GPIOInputs5V4.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 8, 0, 0));
            GPIOInputs5V4.GPIOs.Add(new GPIOObject("GPIO", GPIOObject.GPIOTyp.input, 18, 0, 0));

            m_Inputs.GPIOBanks.Add(GPIOInputs5V);
            m_Inputs.GPIOBanks.Add(GPIOInputs5V8);
            m_Inputs.GPIOBanks.Add(GPIOInputs5V4);


            //  m_OutPuts.createPropertySet(property);
            //   m_Inputs.createPropertySet(property);

            m_GPIOInOutBanks.InOutBanks.Add(m_Inputs);
            m_GPIOInOutBanks.InOutBanks.Add(m_OutPuts);
            m_GPIOInOutBanks.createPropertySet(property);
            return m_GPIOInOutBanks;
        }

        static async public Task<GPIOOInOutBanks> GPIOOInOutBanksAsync(IPropertySet property)
        {
            GPIOOInOutBanks banks = await Task.Run(() => Allocate(property));
            return  banks;;

        }


        public void createPropertySet(IPropertySet property)
        {
            for (int i = 0; i < m_InOutBanks.Count; i++)
            {
                GPIOOBank obj = m_InOutBanks[i];
                obj.createPropertySet(property);

            }

        }

        public void readImages()
        {
            for (int i = 0; i < m_InOutBanks.Count; i++)
            {
                GPIOOBank obj = m_InOutBanks[i];
                obj.readImages();

            }

        }

        public GPIOOBank getGPIOBankByName(string name)
        {

            for (int i = 0; i < m_InOutBanks.Count; i++)
            {
                GPIOOBank obj = m_InOutBanks[i];
                if (obj.BankName == name)
                {
                    return obj;
                }

            }
            return null;

        }

        public ObservableCollection<GPIOOBank> InOutBanks
        {
            get
            {
                return m_InOutBanks;
            }
        }

        public string BankName
        {
            get
            {
                return m_BankName;
            }
            set
            {
                m_BankName = value;
            }

        }

    }
}
