using System;
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
        public DelegateCommand<string> NavigateTabCommand { get; }

        public INavigationService<TabViews> TabNavigation { get; }

        public HomeViewModel(
            INavigationService<MainViews> mainNav,
            INavigationService<TabViews> tabNav)
        {
            TabNavigation = tabNav;
            NavigateExp1Command = new DelegateCommand(() => mainNav.View = MainViews.Experience1View);

            NavigateTabCommand = new DelegateCommand<string>(t => tabNav.View = (TabViews)Enum.Parse(typeof(TabViews), t));

        }
    }
}
