using Microsoft.Practices.Unity;
using Prism.Logging;
using System.Runtime.CompilerServices;

namespace Base.Prism.Interfaces
{
    // TODO: Add proper exception logger that gets the exception as a parameter and outputs all necessary exception info
    public interface ILoggerService : ILoggerFacade
    {
        string CurrentLog { get; }
        void Config(IUnityContainer container);
        void SetLogFile(string logFile);
        void Log(string message, Category category);
        void Log(string message, Category category, string module);
        void Log(string message, Category category, string module, Priority priority, bool system = false,
            [CallerFilePath] string filename = "");
        void SystemLog(string message);
    }
}
