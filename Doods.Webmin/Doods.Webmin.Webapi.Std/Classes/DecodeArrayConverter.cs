using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Webmin.Webapi.Std.Classes
{
    internal class DecodeArrayConverter : JsonConverter
    {
        public static readonly DecodeArrayConverter Singleton = new DecodeArrayConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(List<long>);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            reader.Read();
            var value = new List<long>();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var converter = ParseStringConverter.Singleton;
                var arrayItem = (long) converter.ReadJson(reader, typeof(long), null, serializer);
                value.Add(arrayItem);
                reader.Read();
            }

            return value;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (List<long>) untypedValue;
            writer.WriteStartArray();
            foreach (var arrayItem in value)
            {
                var converter = ParseStringConverter.Singleton;
                converter.WriteJson(writer, arrayItem, serializer);
            }

            writer.WriteEndArray();
        }
    }
}