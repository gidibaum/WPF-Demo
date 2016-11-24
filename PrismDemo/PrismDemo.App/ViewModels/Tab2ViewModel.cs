using Prism.Regions;
using System;
using Prism.Mvvm;

namespace PrismDemo.App.ViewModels
{
    class Tab2ViewModel : BindableBase, INavigationAware
    {
        public string Header { get; } = "Tab2";

        public Tab2ViewModel()
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Console.WriteLine("Navigated From Tab2");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Console.WriteLine("Navigated To Tab2");
        }

    }
}
