using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Services.Core;
using Services.Core.Services;
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

            container.Register(
                Component.For<IIdentifier>().ImplementedBy<Identifier>().LifestyleSingleton(),
                Component.For<IUpdateDataForCitiesThroughEachService>()
                    .ImplementedBy<UpdateDataForCitiesThroughEachService>().LifestyleTransient(),
                Component.For<IGetCitiesService>()
                    .ImplementedBy<GetCitiesService>().LifestyleTransient(),
                Component.For<IAverageDataService>()
                    .ImplementedBy<AverageDataService>().LifestyleTransient()
                );
        }
    }
}