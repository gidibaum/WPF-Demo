using Base.Prism.Interfaces;
using PrismDemo.App.Models;

namespace PrismDemo.App.Views
{
    public partial class HomeView
    {
        public HomeView(INavigationService<TabViews> tabNav)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                tabNav.View = TabViews.Tab1Control;
            };
        }

    }
}
