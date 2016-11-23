using Prism.Regions;
using System;

namespace PrismDemo.App.ViewModels
{
    class TabBViewModel : INavigationAware
    {
        //public string Header { get; } = "Tab2";

        public TabBViewModel()
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Console.WriteLine("Navigated From B");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Console.WriteLine("Navigated To B");
        }

    }
}
