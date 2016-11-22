using Microsoft.Practices.Unity;


namespace Base.Prism.Extensions
{
    public static class ContainerExtensions
    {
        public static IUnityContainer RegisterTypeAsSingelton<TFrom, TTo>(this IUnityContainer container)
            where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());            
        }

        public static IUnityContainer RegisterTypeAsSingelton<TFrom, TTo>(this IUnityContainer container, string name)
           where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>(name, new ContainerControlledLifetimeManager());
        }

    }
}
