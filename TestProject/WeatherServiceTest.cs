using System;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class WeatherServiceTest
    {
        [Test]
        public void FirstTest()
        {
            const string magicString = "http://api.wunderground.com/api/7b9175d0dab642ae/forecast10day/lang:RU/conditions/q/Russia/{0}.xml";
            var req = WebRequest.Create(String.Format(magicString, "Chelyabinsk"));
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            if (stream != null)
            {
                var sr = new StreamReader(stream);
                Console.WriteLine(sr.ReadToEnd());
                sr.Close();
            }
        }
    }
}
