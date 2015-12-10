using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace BalticAmadeus.BuildServer.Domain.Mappings
{
	public class StringClobUserType : IUserType
	{
		public new bool Equals(object x, object y)
		{
			return object.Equals(x, y);
		}

		public int GetHashCode(object x)
		{
			return x.GetHashCode();
		}

		public object NullSafeGet(IDataReader rs, string[] names, object owner)
		{
			return NHibernateUtil.String.NullSafeGet(rs, names[0]);
		}

		public void NullSafeSet(IDbCommand cmd, object value, int index)
		{
			NHibernateUtil.String.NullSafeSet(cmd, value, index);
		}

		public object DeepCopy(object value)
		{
			return value;
		}

		public object Replace(object original, object target, object owner)
		{
			return original;
		}

		public object Assemble(object cached, object owner)
		{
			return cached;
		}

		public object Disassemble(object value)
		{
			return value;
		}

		public SqlType[] SqlTypes
		{
			get { return new[] { NHibernateUtil.StringClob.SqlType }; }
		}

		public Type ReturnedType
		{
			get { return typeof(string); }
		}

		public bool IsMutable
		{
			get { return false; }
		}
	}
}