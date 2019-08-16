﻿
using System;
using System.Globalization;
using System.IO;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Openmedivault.Ssh.Std.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Doods.Openmedivault.Ssh.Std.Requests
{


    public class OmvSerializerException : Exception
    {
        public OmvSerializerException(string message,Exception exception) :base(message,exception)
        {
            
        }
    }
    public class OmvSerializer : IDeserializer
    {
        private readonly JsonSerializer _serializer;
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ValueUnionConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        public OmvSerializer()
        {
            _serializer = new JsonSerializer
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
                DateParseHandling = DateParseHandling.None,

            };
            _serializer.Converters.Add(ValueUnionConverter.Singleton);
            _serializer.Converters.Add(ParseStringConverter.Singleton);
            _serializer.Converters.Add(PrefixConverter.Singleton);
            _serializer.Converters.Add(new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal });
        }


        public T Deserialize<T>(ISshResponse response)
        {
            try
            {
                return Deserialize<T>(response.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new OmvSerializerException(response.Content,e);
            }
           
        }


        public T Deserialize<T>(string json)
        {
            using (var stringReader = new StringReader(json))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    try
                    {
                        return _serializer.Deserialize<T>(jsonTextReader);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Couldn't deserialize json: {json}. Error: {ex}");
                        //Logger.Error($"Couldn't deserialize json: {json}. Error: {ex}");
                        throw;
                    }
                }
            }
        }
    }
}