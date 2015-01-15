using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CompanyIOS
{
	public class NintConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			var valueType = objectType.GetGenericArguments()[1];
			var intermediateDictionaryType = typeof(SortedList<,>).MakeGenericType(typeof(string), valueType);
			var intermediateDictionary = (IDictionary)Activator.CreateInstance(intermediateDictionaryType);
			serializer.Populate(reader, intermediateDictionary);

			var finalDictionary = (IDictionary)Activator.CreateInstance(objectType);
			foreach (DictionaryEntry pair in intermediateDictionary)
				finalDictionary.Add((nint)Convert.ToInt32(pair.Key), pair.Value);

			return finalDictionary;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType.IsNint(typeof(IDictionary<,>)) &&
				objectType.GetGenericArguments()[0].IsNint(typeof(nint)); 
		}
	}

	public class NfloatConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			var valueType = typeof(int);
			var intermediateDictionary = (int)Activator.CreateInstance(valueType);
			serializer.Populate(reader, intermediateDictionary);

			return (nfloat)intermediateDictionary;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType is nint;
		}
	}
}

