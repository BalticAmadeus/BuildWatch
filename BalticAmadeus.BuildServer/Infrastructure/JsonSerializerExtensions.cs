using System.IO;
using Newtonsoft.Json;

namespace BalticAmadeus.BuildServer.Infrastructure
{
	public static class JsonSerializerExtensions
	{
		public static string Serialize(this JsonSerializer serializer, object obj)
		{
			using (var stringWriter = new StringWriter())
			using (var jsonWriter = new JsonTextWriter(stringWriter))
			{
				serializer.Serialize(jsonWriter, obj);
				return stringWriter.ToString();
			}
		}

		public static T Deserialize<T>(this JsonSerializer serializer, string text)
		{
			using (var stringReader = new StringReader(text))
			using (var jsonReader = new JsonTextReader(stringReader))
			{
				return serializer.Deserialize<T>(jsonReader);
			}
		}
	}
}