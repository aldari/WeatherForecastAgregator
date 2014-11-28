using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Services;
using Services.Core.Entities;
using Services.Core.Repositories;
using Services.Core.Services;

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
                .Returns(new[]
                {
                    new City{ Id = 1 },
                    new City{ Id = 2 },
                    new City{ Id = 3 }
                }.AsQueryable());
            var service = new GetCitiesService(cityRepositoryMock.Object);

            var result = service.GetCities();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual(3, result[2].Id);
        }

        [Test]
        public void GetAverageDataForOneCityTest()
        {
            var mockData = new[]
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
            var forecastRepositoryMock = new Mock<IWeatherForecastRepository>();
            forecastRepositoryMock.Setup(m => m.GetAll()).Returns(mockData.AsQueryable());
            var service = new AverageDataService(forecastRepositoryMock.Object);

            var numberGroups = service.GetAverageDataForOneCity(cityId);

            var actual = numberGroups.ToList();

            Assert.AreEqual(new DateTime(2014, 1, 1), actual[0].Date);
            Assert.AreEqual(15, actual[0].AvgTemperature);
            Assert.AreEqual(20, actual[0].AvgHumidity);

            Assert.AreEqual(new DateTime(2014, 1, 2), actual[1].Date);
            Assert.AreEqual(20, actual[1].AvgTemperature);
            Assert.AreEqual(30, actual[1].AvgHumidity);

            Assert.AreEqual(new DateTime(2014, 1, 3), actual[2].Date);
            Assert.AreEqual(30, actual[2].AvgTemperature);
            Assert.AreEqual(50, actual[2].AvgHumidity);
        }

    }
}
