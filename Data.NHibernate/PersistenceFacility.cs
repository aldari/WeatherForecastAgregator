using System;

using Castle.Core.Internal;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;

using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using  Services.Core.Entities;

namespace Data.NHibernate
{
	public class PersistenceFacility : AbstractFacility
	{
		protected virtual void ConfigurePersistence(Configuration config)
		{
			SchemaMetadataUpdater.QuoteTableAndColumns(config);
		}

		protected virtual AutoPersistenceModel CreateMappingModel()
		{
            var m = AutoMap.Assembly(typeof(Entity<int>).Assembly)
				.Where(IsDomainEntity)
				.OverrideAll(ShouldIgnoreProperty)
                .IgnoreBase<Entity<int>>();

			return m;
		}

		protected override void Init()
		{
			var config = BuildDatabaseConfiguration();

			Kernel.Register(
				Component.For<ISessionFactory>()
					.UsingFactoryMethod(_ => config.BuildSessionFactory()),
				Component.For<ISession>()
					.UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
					.LifestylePerWebRequest()
				);
		}

		protected virtual bool IsDomainEntity(Type t)
		{
            return typeof(Entity<int>).IsAssignableFrom(t);
		}

		protected virtual IPersistenceConfigurer SetupDatabase()
		{
			return MsSqlConfiguration.MsSql2008
				.UseOuterJoin()
				.ConnectionString(x => x.FromConnectionStringWithKey("ApplicationServices"))
				.ShowSql();
		}

		public Configuration BuildDatabaseConfiguration()
		{
			return Fluently.Configure()
				.Database(SetupDatabase)
				.Mappings(m => m.AutoMappings.Add(CreateMappingModel()))
				.ExposeConfiguration(ConfigurePersistence)
				.BuildConfiguration();
		}

		private void ShouldIgnoreProperty(IPropertyIgnorer property)
		{
			property.IgnoreProperties(p => p.MemberInfo.HasAttribute<DoNotMapAttribute>());
		}
	}
}