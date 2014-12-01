using NUnit.Framework;
using Services;
using Services.Weather.Impl;

namespace TestProject
{
    public class IdentifierTest
    {
        private Identifier _testObject;
        [SetUp]
        public void TestUp()
        {
            _testObject = new Identifier();
        }

        [Test]
        public void OpenWeatherMapClassReturn1()
        {
            var t = new OpenweathermapService(null);

            var result = _testObject.IdentifierFor(t.GetType());

            Assert.AreEqual(1, result);
        }

        [Test]
        public void WundergroundServiceReturn2()
        {
            var t = new WundergroundService(null);

            var result = _testObject.IdentifierFor(t.GetType());

            Assert.AreEqual(2, result);
        }
    }
}
