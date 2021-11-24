using System;
using Doods.Webmin.Webapi.Std.Datas;
using Newtonsoft.Json;

namespace Doods.Webmin.Webapi.Std.Classes
{
    internal class DiskElementConverter : JsonConverter
    {
        public static readonly DiskElementConverter Singleton = new DiskElementConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(DiskElement) || t == typeof(DiskElement?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new DiskElement {Integer = integerValue};
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new DiskElement {String = stringValue};
            }

            throw new Newtonsoft.Json.JsonSerializationException("Cannot unmarshal type DiskElement");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (DiskElement) untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }

            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }

            throw new Exception("Cannot marshal type DiskElement");
        }
    }
}