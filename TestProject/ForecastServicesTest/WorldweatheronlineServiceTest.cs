using System;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using Services;
using Services.Weather.Impl;

namespace TestProject.ForecastServicesTest
{
    [TestFixture]
    class WorldweatheronlineServiceTest
    {
        [Test]
        public void WorldweatheronlineForecastTest()
        {
            var city = "Chelyabinsk";
            var mockLoader = new Mock<IQueryLoader>();
            mockLoader.Setup(m => m.LoadData(It.IsAny<String>())).Returns(File.ReadAllText(@"mock/worldweatheronline.txt"));
            var worldweatheronlineService = new WorldweatheronlineService(mockLoader.Object);

            var result = worldweatheronlineService.ForecastData(city);

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());
        }


        [Test]
        public void ParseXmlForWorldweatheronlineForecast()
        {
            var city = "Chelyabinsk";
            var mockLoader = new Mock<IQueryLoader>();
            mockLoader.Setup(m => m.LoadData(It.IsAny<String>())).Returns(File.ReadAllText(@"mock/worldweatheronline.txt"));
            var worldweatheronlineService = new WorldweatheronlineService(mockLoader.Object);

            var result = worldweatheronlineService.ForecastData(city);
            var dto = result.First();

            Assert.AreEqual(new DateTime(2014, 12, 1), dto.Date);
            
            Assert.AreEqual(90, dto.Humidity);
            Assert.AreEqual(-7, dto.MaxTemperature);
        }
    }
}
