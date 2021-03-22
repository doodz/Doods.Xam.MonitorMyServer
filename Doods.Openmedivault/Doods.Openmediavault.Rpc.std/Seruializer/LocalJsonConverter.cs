using System.Globalization;
using Doods.Openmedivault.Ssh.Std.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public sealed class LocalJsonConverter : JsonSerializer
    {
        public static readonly JsonSerializer Singleton = new LocalJsonConverter();

        public LocalJsonConverter()
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
            MissingMemberHandling = MissingMemberHandling.Ignore;
            NullValueHandling = NullValueHandling.Include;
            DefaultValueHandling = DefaultValueHandling.Include;
            DateParseHandling = DateParseHandling.None;
            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;

            Converters.Add(ValueUnionConverter.Singleton);
            Converters.Add(ParseStringConverter.Singleton);
            Converters.Add(PrefixConverter.Singleton);
            Converters.Add(new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal});
        }
    }
}