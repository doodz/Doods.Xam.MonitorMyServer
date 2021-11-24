using System;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Seruializer
{
    internal class ValueUnionConverter : JsonConverter
    {
        public static readonly ValueUnionConverter Singleton = new ValueUnionConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(ValueUnion) || t == typeof(ValueUnion?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new ValueUnion {ValueUnionString = stringValue};
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<ValueClass>(reader);
                    return new ValueUnion {ValueClass = objectValue};
            }

            throw new Newtonsoft.Json.JsonSerializationException("Cannot unmarshal type ValueUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ValueUnion) untypedValue;
            if (value.ValueUnionString != null)
            {
                serializer.Serialize(writer, value.ValueUnionString);
                return;
            }

            if (value.ValueClass != null)
            {
                serializer.Serialize(writer, value.ValueClass);
                return;
            }

            throw new Exception("Cannot marshal type ValueUnion");
        }
    }
}