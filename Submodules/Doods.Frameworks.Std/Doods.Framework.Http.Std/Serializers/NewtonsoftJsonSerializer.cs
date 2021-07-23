using System.IO;
using System.Text.RegularExpressions;
using Doods.Framework.Http.Std.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace Doods.Framework.Http.Std.Serializers
{
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        //TODO THE
        // private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly JsonSerializer _serializer;

        public NewtonsoftJsonSerializer()
        {
            ContentType = "application/json";
            _serializer = new JsonSerializer
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Include
            };
        }


        public NewtonsoftJsonSerializer(JsonSerializer serializer)
        {
            ContentType = "application/json";
            _serializer = serializer;
        }

        public string DateFormat { get; set; }

        public string RootElement { get; set; }

        public string Namespace { get; set; }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    _serializer.Serialize(jsonTextWriter, obj);

                    var tmpString = stringWriter.ToString();
                    return Regex.Replace(tmpString, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
                }
            }
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return Deserialize<T>(response.Content);
        }

        public string ContentType { get; set; }

        public T Deserialize<T>(string json)
        {
            using (var stringReader = new StringReader(json))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return _serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }
    }
}