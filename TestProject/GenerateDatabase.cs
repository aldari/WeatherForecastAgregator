using Data.NHibernate;
using NUnit.Framework;

namespace TestProject
{
    public class GenerateDatabase
    {
        [Test]
        public void GenerateDb()
        {
            var o = new GenerateDb();
            o.Execute();
        }
    }
}
