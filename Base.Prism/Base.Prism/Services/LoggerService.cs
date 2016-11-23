using System;
using System.Diagnostics;
using System.Text;
using Prism.Logging;
using System.IO;
using Microsoft.Practices.Unity;
using System.Runtime.CompilerServices;
using Base.Prism.Interfaces;

namespace Base.Prism.Services
{
    public class LogEntry
    {
        public string LogFile { set; get; }
        public string Message { set; get; }

        public LogEntry(string logFile, string message)
        {
            LogFile = logFile;
            Message = message;
        }
    }

    public class LoggerService : ObservableObject, ILoggerService
    {
        public LoggerService()
        {
            
        }

        string Header => "Date; Time; Module; Category; Periority; Message \n";

        #region Property: CurrentLog

        string _CurrentLogFile;
        public string CurrentLog
        {
            get { return _CurrentLogFile; }
            private set
            {
                SetProperty(ref _CurrentLogFile, value);
            }
        }

        #endregion

        public void Log(string message, Category category)
        {
            Log(message, category, Priority.None);
        }

        public void Log(string message, Category category, string module)
        {
            Log(message, category, module, Priority.None);
        }

        public void Log(string message, Category category, Priority priority)
        {
            Log(message, category, "", priority);
        }

        public void SystemLog(string message)
        {
            Log(message, Category.Info, "", Priority.High, true);
        }

        public void Log(string message, Category category, string module, Priority priority,
            bool system = false, [CallerFilePath] string filename = "")
        {
            try
            {
                var str = new StringBuilder();
                str.AppendFormat("{0}", DateTime.Now.ToShortDateString());
                str.AppendFormat("; {0:HH:mm:ss.fff}", DateTime.Now);
                str.AppendFormat("; {0}", module);

                if (system)
                    str.AppendFormat("; {0}", "System");
                else
                    str.AppendFormat("; {0}", category);

                str.AppendFormat("; {0}", priority);
                str.AppendFormat("; {0}\n", message);

                SafeLog(new LogEntry(_CurrentLogFile, str.ToString()));
            }
            catch { }
        }

        void SafeLog(LogEntry logEntry)
        {
            //implement

            Console.WriteLine(logEntry.Message);
        }

        void StartNewLog(string logFile)
        {
            if (logFile == null)
            {
                Log("Logger.StartNewLog can not set new log file to null", Category.Warn);

                return;
            }

            if (!File.Exists(logFile))
            {
                CreatNewLogFile(logFile);
            }

            _CurrentLogFile = logFile;
        }

        void CreatNewLogFile(string logFile)
        {
            File.AppendAllText(logFile, Header);
        }

        void StartSystemLog()
        {
            //implement

            //try
            //{
            //    var appdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            //    var appAsm =  System.Windows.Application.Current.GetType().Assembly; //fix this!!!!

            //    var info = FileVersionInfo.GetVersionInfo(appAsm.Location);

            //    var productName = info.ProductName;
            //    var companyName = info.CompanyName;

            //    var appDataFolder = Path.Combine(appdir, companyName, productName);

            //    if (!Directory.Exists(appDataFolder))
            //        Directory.CreateDirectory(appDataFolder);

            //    var logFile = Path.Combine(appDataFolder, "System.log");

            //    StartNewLog(logFile);
            //}
            //catch
            //{

            //}
        }

        public void Config(IUnityContainer container)
        {
            //implement
        }

        public void SetLogFile(string logFile)
        {
            try
            {
                StartNewLog(logFile);
            }
            catch (Exception e)
            {
                Log(e.Message, Category.Exception);

                Log("LoggerService.SetLogFile failed", Category.Warn);

                StartSystemLog();
            }
        }

       
    }
}
