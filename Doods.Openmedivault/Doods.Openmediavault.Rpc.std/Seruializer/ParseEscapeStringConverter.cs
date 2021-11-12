// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="ParseEscapeStringConverter.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2021
//  </copyright>
//  History:
//   2021/08/05 at 11:50: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System;
using System.IO;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Seruializer
{
    internal class ParseEscapeStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return true;
            //return t == typeof(DiskElement) || t == typeof(DiskElement?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {

            var token = reader.TokenType;
            if (reader.TokenType == JsonToken.Null) return null;
            var stringValue = serializer.Deserialize<string>(reader);

            if(stringValue ==null)
                throw new Newtonsoft.Json.JsonSerializationException($"Cannot unmarshal type Output.content {t}");
            if (string.IsNullOrWhiteSpace(stringValue))
                return stringValue;
            stringValue = stringValue.Replace(@"\\\", string.Empty);
            stringValue = stringValue.Replace(@"\""", "\"");
            var readertest = new JsonTextReader(new StringReader(stringValue));
            try
            {
                var obj = serializer.Deserialize(readertest, t);
                return obj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (token == JsonToken.String)
                return stringValue;
            return null;

        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            serializer.Serialize(writer, untypedValue);

            //throw new Exception("Cannot marshal type DiskElement");
        }
    }
}