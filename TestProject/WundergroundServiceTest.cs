using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Services;

namespace TestProject
{
    [TestFixture]
    public class WeatherServiceTest
    {
        public void FirstTest()
        {
            const string magicString = "http://api.wunderground.com/api/7b9175d0dab642ae/forecast10day/lang:RU/conditions/q/Russia/{0}.xml";
            var uri = String.Format(magicString, "Chelyabinsk");

            var xmld = new QueryLoader().LoadData(uri);
            //Console.WriteLine(xmld);

            File.WriteAllText("check.txt", xmld);
        }

        [Test]
        public void WundergroundForecastTest()
        {
            String content = File.ReadAllText(@"mock/wundergroundAll.txt");
            var wundergroundService = new WundergroundForecast();

            var result = wundergroundService.ForecastData(content);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }

        [Test]
        public void ParseXmlForWundergroundForecast()
        {
            String content = File.ReadAllText(@"mock/wundergroundAll.txt");
            var wundergroundService = new WundergroundForecast();

            var result = wundergroundService.ForecastData(content);
            var dto = result.First();

            Assert.AreEqual(new DateTime(2014, 11, 21), dto.Date);
            Assert.AreEqual("Облачно", dto.Description);
            Assert.AreEqual(81, dto.Humidity);
            Assert.AreEqual(18, dto.WindSpeed);
            Assert.AreEqual("ЗСЗ", dto.WindDirection);
            Assert.AreEqual(-6, dto.MinTemperature);
            Assert.AreEqual(1, dto.MaxTemperature);
        }
    }
}
