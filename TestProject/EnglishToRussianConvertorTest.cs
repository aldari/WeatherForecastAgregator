using NUnit.Framework;
using Services;

namespace TestProject
{
    [TestFixture]
    public class EnglishToRussianConvertorTest
    {
        [Test]
        public void WindDirectionConvertionTest()
        {
            const string input = "NNW";

            var result = input.ToRussianWind();

            Assert.AreEqual("ССЗ", result);
        }
    }
}
