using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WeatherWebApp.Services;

namespace ToBeSeen.Installers
{
	public class ServiceInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Types.FromThisAssembly().Pick()
			                   	.If(Component.IsInSameNamespaceAs<FormsAuthenticationService>())
			                   	.LifestyleTransient()
			                   	.WithService.DefaultInterfaces());
		}
	}
}