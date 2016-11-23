using System;
using Base.Prism.Events;
using Base.Prism.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismDemo.App.Models;
using PrismDemo.Common.Models;

namespace PrismDemo.App.ViewModels
{
    public class MainViewModel : BindableBase, IDisposable
    {

        readonly INavigationService<MainViews> _MainNavigation;
        readonly IEventAggregator _EventAggregator;

        public DelegateCommand NavigateExp1Command { get; }

        public MainViewModel(
            INavigationService<MainViews> nav,
            IEventAggregator events)
        {
            _MainNavigation = nav;
            _EventAggregator = events;

            NavigateExp1Command = new DelegateCommand(() => nav.View = MainViews.Experience1View);            
        }

        public void Init()
        {
            _MainNavigation.View = MainViews.HomeView;
        }

        public void Dispose()
        {
            _EventAggregator.GetEvent<ShutDownEvent>().Publish(true);
        }
    }
}
