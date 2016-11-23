using Base.Prism.Interfaces;
using PrismDemo.App.Models;

namespace PrismDemo.App.Views
{
    public partial class HomeView
    {
        public HomeView(ITabNavigationService<TabViews> tabNav)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                tabNav.InitTabItems();
                tabNav.View = TabViews.Tab1Control;
            };
        }

    }
}
