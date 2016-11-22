
using Microsoft.Practices.Unity;
using System.Windows.Controls;
using Base.Prism.Interfaces;
using Base.Prism.Services;


namespace Base.Prism.Extensions
{
    public static class NavigationExtensions
    {
        public static void RegisterNavigationService<T>(this IUnityContainer container, string regionName)
        {
            container.RegisterType<INavigationService<T>, NavigationService<T>>(
                new ContainerControlledLifetimeManager(),
                new InjectionProperty("RegionName", regionName));
        }

        public static void RegisterNavigationService<TNavInterface, TNavService>(this IUnityContainer container, string regionName) 
            where TNavService : TNavInterface
        {
            container.RegisterType<TNavInterface, TNavService>(
                new ContainerControlledLifetimeManager(),
                new InjectionProperty("RegionName", regionName));
        }

        //public static void RegisterTabNavigationService<T>(this IUnityContainer container, string regionName)
        //{
        //    container.RegisterType<ITabNavigationService<T>, TabNavigationService<T>>(
        //        new ContainerControlledLifetimeManager(),
        //        new InjectionProperty("RegionName", regionName));
        //}

        public static void RegisterNavigationControl<T>(this IUnityContainer container) where T : Control
        {
            container.RegisterType<Control, T>(typeof(T).Name, new ContainerControlledLifetimeManager());
        }

        public static void RegisterSingletonType<TFrom, TTo>(this IUnityContainer container) where TTo : TFrom
        {
            container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }

    }
}
