using System;
using System.Collections.Generic;
using System.Linq;
using Doods.Framework.Ssh.Std.Base;
using Doods.Framework.Ssh.Std.Converters;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Ssh.Std.Serializers;
using Doods.Framework.Std.Extensions;

namespace Doods.Framework.Ssh.Std.Requests.YetRequest
{
    /// <summary>
    /// </summary>
    // <example>
    // pi@raspberrypi:~ $ nohup sudo apt-get update >/dev/null 2>&1 </dev/null &
    // [1] 1144
    // </example>
    public class NoHupRequest : SshRequestBase
    {
        //public const string RequestString = $"nohup {cmd} >/dev/null 2>&1 </dev/null & echo {ReturnQuery.ResultPid} $! &";
        public NoHupRequest(string cmd) : base(
            $"nohup {cmd} >/dev/null 2>&1 </dev/null & echo {ReturnQuery.ResultPid} $! &")
        {
            _SshSerializer = new SshSerializer(new SshSerializerSettings
            {
                Converters = new List<ISshConverter> {new NoHupRequestConverter(cmd), new SshToSimpleStringConverter()}
            });
        }

        public NoHupRequest(ISshRequest request) : this(request.CommandText)
        {
        }
        //private readonly IList<ISshConverter> _converters = new List<ISshConverter>() { new NoHupRequestConverter(), new SshToSimpleStringConverter() };
        //internal static readonly ISynchronizedCache SynchronizedPIdsCache = new SynchronizedCache();


        private class NoHupRequestConverter : ISshConverter
        {
            private readonly string _cmd;

            public NoHupRequestConverter(string cmd)
            {
                _cmd = cmd.RemoveAllWhitespace("_");
            }

            public bool CanConvert(Type objectType)
            {
                if (objectType == typeof(string)) return true;
                if (objectType == typeof(int)) return true;
                return false;
            }

            public object Read(string reader, Type objectType)
            {
                var tab = reader.Trim().Split(new[] {ReturnQuery.ResultPid}, StringSplitOptions.RemoveEmptyEntries);
                if (!tab.Any()) return GetDefaultValue(objectType);
                var b = int.TryParse(tab?.Last().Trim(), out var result);

                //SynchronizedPIdsCache.AddOrUpdate(_cmd, result.ToString());
                if (objectType == typeof(int)) return result;
                return tab?.Last().Trim();
            }

            private object GetDefaultValue(Type t)
            {
                if (t.IsValueType)
                    return Activator.CreateInstance(t);

                return null;
            }
        }
    }
}