using Prism.Regions;
using System;
using Prism;
using Prism.Mvvm;

namespace PrismDemo.App.ViewModels
{
    class Tab1ViewModel : BindableBase, INavigationAware, IActiveAware
    {
        public event EventHandler IsActiveChanged;

        public string Header { get; } = "Tab1";

        public Tab1ViewModel()
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Console.WriteLine("Navigated From Tab1");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Console.WriteLine("Navigated To Tab1");
        }

        #region Property: IsActive

        bool _IsActive;

        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                _IsActive = value;
                if (_IsActive)
                    Console.WriteLine($"{Header} is active");
            }
        }

        #endregion
    }
}
