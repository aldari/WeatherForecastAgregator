using NUnit.Framework;
using Services;
using System;
using System.IO;
using System.Linq;

namespace TestProject
{
    public class OpenweathermapServiceTest 
    {
        public void Test()
        {
            const string magicString = "http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&mode=xml&units=metric&cnt=10&lang=ru";
            var uri = String.Format(magicString, "Chelyabinsk");

            var xmld = new QueryLoader().LoadData(uri);
            File.WriteAllText("openweathermap.txt", xmld);
        }

        [Test]
        public void OpenweathermapServiceReturnForecastsTest()
        {
            String content = File.ReadAllText(@"mock/openweathermap.txt");

            var service = new OpenweathermapService();
            var result = service.ForecastData(content);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }

        [Test]
        public void XmlNodeParseTest()
        {
            String content = File.ReadAllText(@"mock/openweathermap.txt");
            var service = new OpenweathermapService();

            var result = service.ForecastData(content);
            var dto = result.First();
            
            Assert.AreEqual(new DateTime(2014, 11, 23), dto.Date);
            Assert.AreEqual("небольшой снегопад", dto.Description);
            Assert.AreEqual(92, dto.Humidity);
            Assert.AreEqual(-13, dto.MaxTemperature);
            Assert.AreEqual(-22, dto.MinTemperature);
            Assert.AreEqual("ССВ", dto.WindDirection);
            Assert.AreEqual(7, dto.WindSpeed);
        }
    }
}
