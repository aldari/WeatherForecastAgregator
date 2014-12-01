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
        public void WundergroundForecastTest()
        {
            var city = "Chelyabinsk";
            var mockLoader = new Mock<IQueryLoader>();
            mockLoader.Setup(m => m.LoadData(It.IsAny<String>())).Returns(File.ReadAllText(@"mock/worldweatheronline.txt"));
            var worldweatheronlineService = new WorldweatheronlineService(mockLoader.Object);

            var result = worldweatheronlineService.ForecastData(city);

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());
        }

    }
}
