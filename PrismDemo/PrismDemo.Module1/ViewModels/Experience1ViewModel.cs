using System;
using Base.Prism.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismDemo.Common.Models;
using PrismDemo.Common.Services;

namespace PrismDemo.Module1.ViewModels
{
    class Experience1ViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand NavigateHomeCommand { get; }
        public DelegateCommand CounterCommand { get; }

        public ICounterService CounterService { get; }

        public Experience1ViewModel(
            INavigationService<MainViews> nav, 
            ICounterService counterService)
        {
            CounterService = counterService;
            NavigateHomeCommand = new DelegateCommand(() => nav.View = MainViews.HomeView);
            CounterCommand = new DelegateCommand(CounterService.Click);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Console.WriteLine("Hello Experience1");
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Console.WriteLine("Bye Experience1");
        }
    }
}
