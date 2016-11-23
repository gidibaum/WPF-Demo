using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Prism.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using PrismDemo.App.Models;
using PrismDemo.Common.Models;

namespace PrismDemo.App.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        public DelegateCommand NavigateExp1Command { get; }

        public ITabNavigationService<TabViews> TabNavigation { get; }

        public HomeViewModel(
            INavigationService<MainViews> mainNav,
            ITabNavigationService<TabViews> tabNav)
        {
            TabNavigation = tabNav;
            NavigateExp1Command = new DelegateCommand(() => mainNav.View = MainViews.Experience1View);
        }
    }
}
