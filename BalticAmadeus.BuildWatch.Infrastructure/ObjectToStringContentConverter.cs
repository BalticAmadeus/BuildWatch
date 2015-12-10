using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BalticAmadeus.BuildWatch.Infrastructure
{
	public static class ObjectToHttpContentExtensions
	{
		public static StringContent AsJsonStringContent<T>(this T data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			return new StringContent(jsonString, Encoding.UTF8, "application/json");
		}

		public static StringContent AsXmlStringContent<T>(this T data)
		{
			using (var stringWriter = new StringWriter())
			{
				new XmlSerializer(typeof(T)).Serialize(stringWriter, data);
				return new StringContent(stringWriter.ToString(), Encoding.UTF8, "application/xml");
			}
		}
	}

	public static class HttpContentToObjectExtensions
	{
		public static T As<T>(this HttpContent content)
		{
			T result = default(T);


			var contentString = content.ReadAsStringAsync().Result;

			if (TryParseJson(contentString, out result))
				return result;
			if (TryParseXml(contentString, out result))
				return result;

			throw new NotSupportedException();
		}

		private static bool TryParseJson<T>(string content, out T result)
		{
			try
			{
				result = JsonConvert.DeserializeObject<T>(content);
				return true;
			}
			catch (Exception)
			{
				result = default(T);
				return false;
			}
		}

		private static bool TryParseXml<T>(string content, out T result)
		{
			try
			{
				result = (T)new XmlSerializer(typeof(T)).Deserialize(new StringReader(content));
				return true;
			}
			catch
			{
			}

			result = default(T);
			return false;
		}
	}
}
