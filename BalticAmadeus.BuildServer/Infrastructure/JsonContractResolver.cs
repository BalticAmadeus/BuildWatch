using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace BalticAmadeus.BuildServer.Infrastructure
{
	public class JsonContractResolver : DefaultContractResolver
	{
		protected override List<MemberInfo> GetSerializableMembers(Type objectType)
		{
			return objectType.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToList();
		}
	}
}