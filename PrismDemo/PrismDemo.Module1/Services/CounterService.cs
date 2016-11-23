using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Prism.Events;
using Base.Prism.Interfaces;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using PrismDemo.Common.Services;

namespace PrismDemo.Module1.Services
{
    public class CounterService : BindableBase, ICounterService
    {
        #region Property: Counter

        int _Counter;

        public int Counter
        {
            get { return _Counter; }
            private set { SetProperty(ref _Counter, value); }
        }

        #endregion

        readonly ILoggerService _Logger;

        public CounterService(ILoggerService logger, IEventAggregator events)
        {
            _Logger = logger;

            events.GetEvent<ShutDownEvent>().Subscribe(b => ShutDown());
        }

        public void Click()
        {
            Counter++;
            _Logger.Log($"Counter = {Counter}", Category.Info);
        }

        void ShutDown()
        {
            _Logger.Log("Counter Service Sutting Down!", Category.Debug);
        }
    }
}
