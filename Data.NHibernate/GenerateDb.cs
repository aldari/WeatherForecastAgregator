using NHibernate.Tool.hbm2ddl;

namespace Data.NHibernate
{
    public class GenerateDb
    {
        public void Execute()
        {
            var scheme = new SchemaExport(new PersistenceFacility().BuildDatabaseConfiguration());
            scheme.Drop(false, true);
            scheme.Create(false, true);
        }
    }
}
