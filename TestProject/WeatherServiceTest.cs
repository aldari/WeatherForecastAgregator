using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Moq;
using NUnit.Framework;
using Services;

namespace TestProject
{
    [TestFixture]
    public class WeatherServiceTest
    {
        [Test]
        public void FirstTest()
        {
            const string magicString = "http://api.wunderground.com/api/7b9175d0dab642ae/forecast10day/lang:RU/conditions/q/Russia/{0}.xml";
            var uri = String.Format(magicString, "Chelyabinsk");

            var xmld = new QueryLoader().LoadData(uri);
            //Console.WriteLine(xmld);

            File.WriteAllText("check.txt", xmld);
        }

        [Test]
        public void ThirdTest()
        {
            String str = File.ReadAllText(@"mock/wundergroundNode.txt");
            var node = XElement.Parse(str);
            
            var date = new DateTime(
                int.Parse(node.Element("date").Element("year").Value),
                int.Parse(node.Element("date").Element("month").Value),
                int.Parse(node.Element("date").Element("day").Value)
            );
            var description = node.Element("conditions").Value;
            var humidity = int.Parse(node.Element("avehumidity").Value);
            var windSpeed = int.Parse(node.Element("avewind").Element("kph").Value);
            var windDirection = node.Element("avewind").Element("dir").Value;
            var minTemperature = int.Parse(node.Element("low").Element("celsius").Value);
            var maxTemperature = int.Parse(node.Element("high").Element("celsius").Value);

            Assert.AreEqual(new DateTime(2014, 11, 21), date);
            Assert.AreEqual("Облачно", description);
            Assert.AreEqual(81, humidity);
            Assert.AreEqual(18, windSpeed);
            Assert.AreEqual("ЗСЗ", windDirection);
            Assert.AreEqual(-6, minTemperature);
            Assert.AreEqual(1, maxTemperature);
        }

        [Test]
        public void WundergroundForecastTest()
        {
            String str = File.ReadAllText(@"mock/wundergroundAll.txt");
            var wundergroundService = new WundergroundForecast();

            var result = wundergroundService.ForecastData(str);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }

        [Test]
        public void ParseXmlForWundergroundForecast()
        {
            String str = File.ReadAllText(@"mock/wundergroundNode.txt");
            var wundergroundService = new WundergroundForecast();

            var node = XElement.Parse(str);
            var dto = wundergroundService.ParseNode(node);

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
