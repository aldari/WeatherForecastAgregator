using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Data.NHibernate.Repository;
using Services.Core.Repositories;

namespace Data.NHibernate
{
	public class PersistenceInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.AddFacility<PersistenceFacility>();
		    container.Register(
                Component.For<IWeatherForecastRepository>().ImplementedBy<NhWeatherForecastRepository>(),
                Component.For<IForecastServiceEntityRepository>().ImplementedBy<NhForecastServiceRepository>(),
                Component.For<ICityRepository>().ImplementedBy<NhCityRepository>()
		        );
		}
	}
}