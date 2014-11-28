using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Services.Core;
using Services.Core.Entities;
using Services.Core.Services;
using Services.Weather;

namespace TestProject
{
    public class InjectionsTest
    {
        void FirstTest()
        {
            var windsorContainer = new WindsorContainer();

            windsorContainer.Install(
                FromAssembly.Named("Data.NHibernate"),
                FromAssembly.Named("Services")
                );

            var service = windsorContainer.Resolve<IGetCitiesService>();
            var data = service.GetCities();
            foreach (var dto in data)
                Console.WriteLine(dto.Name);
            windsorContainer.Dispose();
        }

        void SecondTest()
        {
            var windsorContainer = new WindsorContainer();

            windsorContainer.Install(
                FromAssembly.Named("Data.NHibernate"),
                FromAssembly.Named("Services")
                );

            var service = windsorContainer.Resolve<IUpdateDataForCitiesThroughEachService>();
            var fs = windsorContainer.Resolve<IForecastService>();
            service.ProceedCity(fs, new City{Id = 0, Name = ""});
            windsorContainer.Dispose();
        }

        void ThirdTest()
        {
            var windsorContainer = new WindsorContainer();

            windsorContainer.Install(
                FromAssembly.Named("Data.NHibernate"),
                FromAssembly.Named("Services")
                );

            var service = windsorContainer.Resolve<IAverageDataService>();
            var data = service.GetAverageDataForOneCity(3);
            foreach (var dto in data)
            {
                Console.WriteLine(dto.Date);
                Console.WriteLine(dto.AvgHumidity);
                Console.WriteLine(dto.AvgTemperature);
            }
            windsorContainer.Dispose();
        }
    }
}
