using Prism.Regions;
using System;

namespace TabNavigationDemo.Views
{
 
    public partial class Tab1Control : INavigationAware
    {
        public string Header { get; } = "Tab1";

        public Tab1Control()
        {
            InitializeComponent();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Console.WriteLine("OnNavigated From {0}", navigationContext.Uri);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Console.WriteLine("OnNavigated To {0}", navigationContext.Uri);
        }
    }
}
