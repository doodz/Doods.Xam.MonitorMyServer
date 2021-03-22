using System;
using System.Globalization;
using System.IO;
using Doods.Framework.Ssh.Std.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class OmvSerializer : IDeserializer
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ValueUnionConverter.Singleton,
                new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
            }
        };

        private readonly JsonSerializer _serializer;

        public OmvSerializer()
        {
            //_serializer = new JsonSerializer
            //{
            //    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            //    MissingMemberHandling = MissingMemberHandling.Ignore,
            //    NullValueHandling = NullValueHandling.Ignore,
            //    DefaultValueHandling = DefaultValueHandling.Include,
            //    DateParseHandling = DateParseHandling.None,
            //    StringEscapeHandling =StringEscapeHandling.EscapeNonAscii

            //};
            //_serializer.Converters.Add(ValueUnionConverter.Singleton);
            //_serializer.Converters.Add(ParseStringConverter.Singleton);
            //_serializer.Converters.Add(PrefixConverter.Singleton);
            //_serializer.Converters.Add(new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal });
            _serializer = LocalJsonConverter.Singleton;
        }

        public T DeserializeError<T>(IApiResponse apiResponse)
        {
            //  var obj = deserializer.Deserialize <ResponseError<OMVError>>(apiResponse.ErrorMessage);
            try
            {
                return Deserialize<T>(apiResponse.ErrorMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new OmvSerializerException(apiResponse.ErrorMessage, e);
            }
        }

        public T Deserialize<T>(IApiResponse apiResponse)
        {
            try
            {
                return Deserialize<T>(apiResponse.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw new OmvSerializerException($"OmvSerialiser :{Environment.NewLine}" +
                                                 $"{apiResponse.Content}", e);
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
                        Console.WriteLine(
                            $"OmvSerialiser :{Environment.NewLine} Couldn't deserialize json: {json} to {typeof(T)}. Error: {ex}");
                        //Logger.Error($"Couldn't deserialize json: {json}. Error: {ex}");
                        throw;
                    }
                }
            }
        }
    }
}