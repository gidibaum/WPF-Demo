
using Microsoft.Practices.Unity;
using System.Windows.Controls;
using Base.Prism.Interfaces;
using Base.Prism.Services;
using Prism.Regions;


namespace Base.Prism.Extensions
{
    public static class NavigationExtensions
    {
        public static IUnityContainer RegisterNavigationService<T>(this IUnityContainer container, string regionName)
        {
            container.RegisterType<INavigationService<T>, NavigationService<T>>(
                new ContainerControlledLifetimeManager(),
                new InjectionProperty("RegionName", regionName));

            return container;
        }

        public static IUnityContainer RegisterNavigationService<TNavInterface, TNavService>(this IUnityContainer container, string regionName)
            where TNavService : TNavInterface
        {
            container.RegisterType<TNavInterface, TNavService>(
                new ContainerControlledLifetimeManager(),
                new InjectionProperty("RegionName", regionName));

            return container;
        }

        //public static IUnityContainer RegisterTabNavigationService<T>(this IUnityContainer container, string regionName)
        //{
        //    container.RegisterType<ITabNavigationService<T>, TabNavigationService<T>>(
        //        new ContainerControlledLifetimeManager(),
        //        new InjectionProperty("RegionName", regionName));

        //    return container;
        //}

        //public static void RegisterNavigationControl<T>(this IUnityContainer container) where T : Control
        //{
        //    container.RegisterSingletonType<Control, T>(typeof(T).Name);
        //}

        public static void RegisterNavigationControl<T>(this IUnityContainer container)
        {
            container.RegisterSingletonType<object, T>(typeof(T).Name);
        }


        public static void RegisterSingletonType<TFrom, TTo>(this IUnityContainer container) where TTo : TFrom
        {
            container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }

        public static void RegisterSingletonType<TFrom, TTo>(this IUnityContainer container, string name) where TTo : TFrom
        {
            container.RegisterType<TFrom, TTo>(name, new ContainerControlledLifetimeManager());
        }


        public static IRegionManager RegisterNavigationControlWithRegion<T>(this IRegionManager manager, string region) where T : Control
        {
            return manager.RegisterViewWithRegion(region, typeof(T));
        }

        public static IUnityContainer RegisterNavigationControlWithRegion<T>(this IUnityContainer container, string regionName) where T : Control
        {
            var region = container.Resolve<IRegionManager>();
            region.RegisterViewWithRegion(regionName, typeof(T));
            return container;
        }

    }
}
