using System;

namespace Data.NHibernate
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class DoNotMapAttribute : Attribute
	{
	}
}