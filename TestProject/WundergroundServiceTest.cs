using System;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using Services;
using Services.Weather.Impl;

namespace TestProject
{
    [TestFixture]
    public class WeatherServiceTest
    {
        [Test]
        public void ParseXmlForWundergroundForecast()
        {
            var city = "Chelyabinsk";
            var mockLoader = new Mock<IQueryLoader>();
            mockLoader.Setup(m => m.LoadData(It.IsAny<String>())).Returns(File.ReadAllText(@"mock/wundergroundAll.txt"));
            var wundergroundService = new WundergroundForecast(mockLoader.Object);

            var result = wundergroundService.ForecastData(city);
            var dto = result.First();

            Assert.AreEqual(new DateTime(2014, 11, 21), dto.Date);
            Assert.AreEqual("Облачно", dto.Description);
            Assert.AreEqual(81, dto.Humidity);
            Assert.AreEqual(18, dto.WindSpeed);
            Assert.AreEqual("ЗСЗ", dto.WindDirection);
            Assert.AreEqual(-6, dto.MinTemperature);
            Assert.AreEqual(1, dto.MaxTemperature);
        }

        [Test]
        public void TestCommonUsage()
        {
            var city = "Chelyabinsk";
            var mockLoader = new Mock<IQueryLoader>();
            mockLoader.Setup(m => m.LoadData(It.IsAny<String>())).Returns(File.ReadAllText(@"mock/wundergroundAll.txt"));
            var wundergroundService = new WundergroundForecast(mockLoader.Object);

            var result = wundergroundService.ForecastData(city);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }
    }
}
