using System;
using Doods.Openmediavault.Rpc.Std.Data.V4.FileSystem;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Seruializer
{
    internal class PrefixConverter : JsonConverter
    {
        public static readonly PrefixConverter Singleton = new PrefixConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(Prefix) || t == typeof(Prefix?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new Prefix {Integer = integerValue};
                case JsonToken.Boolean:
                    var boolValue = serializer.Deserialize<bool>(reader);
                    return new Prefix {Bool = boolValue};
            }

            throw new Newtonsoft.Json.JsonSerializationException("Cannot unmarshal type Prefix");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Prefix) untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }

            if (value.Bool != null)
            {
                serializer.Serialize(writer, value.Bool.Value);
                return;
            }

            throw new Exception("Cannot marshal type Prefix");
        }
    }
}