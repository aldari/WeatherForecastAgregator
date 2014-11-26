using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Services;

namespace TestProject
{
    [TestFixture]
    class WorldweatheronlineServiceTest
    {
        public void Test()
        {
            const string magicString = "http://api.worldweatheronline.com/free/v1/weather.ashx?q={0}&format=xml&num_of_days=5&key=7a7ca84ff9b809b9731e3bb53a421";
            var uri = String.Format(magicString, "Chelyabinsk");

            var xmld = new QueryLoader().LoadData(uri);
            //Console.WriteLine(xmld);

            File.WriteAllText("worldweatheronlineAll.txt", xmld);
        }

        [Test]
        public void WundergroundForecastTest()
        {
            String content = File.ReadAllText(@"mock/worldweatheronlineAll.txt");
            var wundergroundService = new WorldweatheronlineService();

            var result = wundergroundService.ForecastData(content);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }

    }
}
