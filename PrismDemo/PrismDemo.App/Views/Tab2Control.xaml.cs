using Prism.Regions;
using System;

namespace TabNavigationDemo.Views
{
 
    public partial class Tab2Control : INavigationAware
    {
        public string Header { get; } = "Tab2";

        public Tab2Control()
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
