using Castle.Core;
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
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
            container.Register(Component.For<NhUnitOfWorkInterceptor>().LifeStyle.Transient);

			container.AddFacility<PersistenceFacility>();
		    container.Register(
                Component.For<IWeatherForecastRepository>().ImplementedBy<NhWeatherForecastRepository>(),
                Component.For<IForecastServiceEntityRepository>().ImplementedBy<NhForecastServiceRepository>(),
                Component.For<ICityRepository>().ImplementedBy<NhCityRepository>()
		        );
		}

        void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            //Intercept all methods of all repositories.
            if (UnitOfWorkHelper.IsRepositoryClass(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(NhUnitOfWorkInterceptor)));
            }

            //Intercept all methods of classes those have at least one method that has UnitOfWork attribute.
            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
            {
                if (UnitOfWorkHelper.HasUnitOfWorkAttribute(method))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(NhUnitOfWorkInterceptor)));
                    return;
                }
            }
        }
	}
}