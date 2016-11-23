using System;
using Base.Prism.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using PrismDemo.Common.Models;

namespace PrismDemo.App.ViewModels
{
    public class MainViewModel : BindableBase, IDisposable
    {
        readonly INavigationService<MainViews> _MainNavigation;

        public DelegateCommand NavigateExp1Command { get; }

        public MainViewModel(INavigationService<MainViews> nav)
        {
            _MainNavigation = nav;

            NavigateExp1Command = new DelegateCommand(() => nav.View = MainViews.Experience1View);            
        }

        public void Init()
        {
            _MainNavigation.View = MainViews.HomeView;
        }

        public void Dispose()
        {
            
        }
    }
}
