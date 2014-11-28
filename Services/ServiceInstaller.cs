using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Services.Weather;
using Services.Weather.Impl;

namespace Services
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IQueryLoader>().ImplementedBy<QueryLoader>().LifestyleTransient(),

                Component.For<IForecastService>().ImplementedBy<WundergroundService>().LifestyleTransient(),
                Component.For<IForecastService>().ImplementedBy<OpenweathermapService>().LifestyleTransient()
                );

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
        }
    }
}