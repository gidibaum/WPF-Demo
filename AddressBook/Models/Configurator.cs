using Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using AddressBook.ViewModels;

namespace AddressBook.Models
{
    public class Configurator : ObservableObject
    {
        #region Property: ProductName

        string m_ProductName;
        public string ProductName
        {
            get { return m_ProductName; }
            set
            {
                m_ProductName = value;
                RaisePropertyChanged("ProductName");
            }
        }

        #endregion

        #region Property: Version

        string m_Version;
        public string Version
        {
            get { return m_Version; }
            set
            {
                m_Version = value;
                RaisePropertyChanged("Version");
            }
        }

        #endregion

        #region Property: CompanyName

        string m_CompanyName;
        public string CompanyName
        {
            get { return m_CompanyName; }
            set
            {
                m_CompanyName = value;
                RaisePropertyChanged("CompanyName");
            }
        }

        #endregion

        string AppDataFolder { get; set; }
        string AppExeFolder { get; set; }
        string ConfigFileName { get; set; }


        #region Property: Number

        int _Number;

        public int Number
        {
            get { return _Number; }
            set { SetProperty(ref _Number, value); }
        }

        #endregion


        public Configurator()
        {
        }

        public void Initialize()
        {
            var appdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var appAsm = Application.Current.GetType().Assembly;

            
            var appVersion = appAsm.GetName().Version;
            if (appAsm.Location != null)
            {
                var info = FileVersionInfo.GetVersionInfo(appAsm.Location);

                Version = $"{appVersion.Major}.{appVersion.Minor}";
                ProductName = info.ProductName;
                CompanyName = info.CompanyName;
            }

            AppDataFolder = Path.Combine(appdir, CompanyName, ProductName);

            AppExeFolder = Path.GetDirectoryName(appAsm.Location);

            Debug.Assert(AppExeFolder != null, "AppExeFolder != null");

            ConfigFileName = Path.Combine(AppExeFolder, "Data", "Config.xml");


            var xml = XElement.Load(ConfigFileName);

            Number = xml.GetAttribute<int>(nameof(Number));

        }
    }
}
