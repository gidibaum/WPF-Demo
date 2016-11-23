using Prism.Regions;
using System;
using Prism.Mvvm;

namespace PrismDemo.App.ViewModels
{
    class TabAViewModel: BindableBase, INavigationAware
    {
        //public string Header { get; } = "Tab1";

        public TabAViewModel()
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Console.WriteLine("Navigated From A");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Console.WriteLine("Navigated To A");
        }
    }
}
