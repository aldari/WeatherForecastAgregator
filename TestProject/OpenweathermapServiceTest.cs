using Moq;
using NUnit.Framework;
using Services;
using System;
using System.IO;
using System.Linq;

namespace TestProject
{
    public class OpenweathermapServiceTest
    {
        [Test]
        public void XmlNodeParseTest()
        {
            const string city = "Chelyabinsk";
            var mockLoader = new Mock<IQueryLoader>();
            mockLoader.Setup(m => m.LoadData(It.IsAny<String>())).Returns(File.ReadAllText(@"mock/openweathermap.txt"));
            var service = new OpenweathermapService(mockLoader.Object);

            var result = service.ForecastData(city);
            var dto = result.First();
            
            Assert.AreEqual(new DateTime(2014, 11, 23), dto.Date);
            Assert.AreEqual("небольшой снегопад", dto.Description);
            Assert.AreEqual(92, dto.Humidity);
            Assert.AreEqual(-13, dto.MaxTemperature);
            Assert.AreEqual(-22, dto.MinTemperature);
            Assert.AreEqual("ССВ", dto.WindDirection);
            Assert.AreEqual(7, dto.WindSpeed);
        }

        [Test]
        public void TestCommonUsage()
        {
            const string city = "Chelyabinsk";
            var mockLoader = new Mock<IQueryLoader>();
            mockLoader.Setup(m => m.LoadData(It.IsAny<String>())).Returns(File.ReadAllText(@"mock/openweathermap.txt"));
            var service = new OpenweathermapService(mockLoader.Object);

            var result = service.ForecastData(city);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }
    }
}
