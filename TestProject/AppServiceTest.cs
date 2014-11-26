using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Services;
using Services.Core.Entities;
using Services.Core.Repositories;

namespace TestProject
{
    [TestFixture]
    public class AppServiceTest
    {
        [Test]
        public void GetCitiesTest()
        {
            var cityRepositoryMock = new Mock<ICityRepository>();
            cityRepositoryMock.Setup(m => m.GetAll())
                .Returns(new City[]
                {
                    new City{ Id = 1 },
                    new City{ Id = 2 },
                    new City{ Id = 3 }
                }.AsQueryable());
            var service = new AppService(cityRepositoryMock.Object);

            var result = service.GetCities();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual(3, result[2].Id);
        }

        [Test]
        public void GetDataForOneCity()
        {
            var fakeData = new WeatherForecast[]
            {
                //First City
                // First Service
                new WeatherForecast
                {
                    City = new City{ Id = 1},
                    Date = new DateTime(2014, 1, 1),
                    DayTemperature = 10,
                    Humidity = 10,
                    Service = new ForecastServiceEntity{ Id = 1}
                },
                new WeatherForecast
                {
                    City = new City{ Id = 1},
                    Date = new DateTime(2014, 1, 2),
                    DayTemperature = 15,
                    Humidity = 20,
                    Service = new ForecastServiceEntity{ Id = 1}
                },
                // Second Service
                new WeatherForecast
                {
                    City = new City{ Id = 1},
                    Date = new DateTime(2014, 1, 1),
                    DayTemperature = 20,
                    Humidity = 30,
                    Service = new ForecastServiceEntity{ Id = 2}
                },
                new WeatherForecast
                {
                    City = new City{ Id = 1},
                    Date = new DateTime(2014, 1, 2),
                    DayTemperature = 25,
                    Humidity = 40,
                    Service = new ForecastServiceEntity{ Id = 2}
                },
                new WeatherForecast
                {
                    City = new City{ Id = 1},
                    Date = new DateTime(2014, 1, 3),
                    DayTemperature = 30,
                    Humidity = 50,
                    Service = new ForecastServiceEntity{ Id = 2}
                },
                //Second City
                new WeatherForecast
                {
                    City = new City{ Id = 2},
                    Date = new DateTime(2014, 1, 1),
                    DayTemperature = 35,
                    Humidity = 60,
                    Service = new ForecastServiceEntity{ Id = 1}
                }
            };

            const int cityId = 1;
            var numberGroups =
            from n in fakeData.Where(c => c.City.Id == cityId)
            group n by n.Service.Id into g
            select new { ServiceNumber = g.Key, avgTemperature = g.Average(x=>x.DayTemperature), avgHumidity = g.Average(x=>x.Humidity) };
            foreach (var numberGroup in numberGroups)
            {
                Console.WriteLine(numberGroup.ServiceNumber);
                Console.WriteLine(numberGroup.avgTemperature);
                Console.WriteLine(numberGroup.avgHumidity);
            }
//            var mock = new Mock<IWeatherForecastRepository>();
//            mock.Setup(m => m.GetAll()).Returns(fakeData.AsQueryable());


        }
    }
}
