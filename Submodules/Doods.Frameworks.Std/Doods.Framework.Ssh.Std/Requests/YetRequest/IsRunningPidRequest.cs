using System;
using System.Collections.Generic;
using Doods.Framework.Ssh.Std.Converters;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Requests.YetRequest
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     pi @raspberrypi:~ $ ps -p 42
    ///     PID TTY          TIME CMD
    ///     42 ?        00:00:00 nfsiod
    ///     pi@raspberrypi:~ $ ps -p 1052334
    ///     PID TTY          TIME CMD
    /// </example>
    public class IsRunningPidRequest : SshRequestBase
    {
        public IsRunningPidRequest(string pid) : base($"ps -p {pid}")
        {
            _SshSerializer = new SshSerializer(new SshSerializerSettings
            {
                Converters = new List<ISshConverter>
                    {new RunningPidRequestConverter(pid), new SshToSimpleStringConverter()}
            });
        }

        public IsRunningPidRequest(int pid) : base($"ps -p {pid}")
        {
            _SshSerializer = new SshSerializer(new SshSerializerSettings
            {
                Converters = new List<ISshConverter>
                    {new RunningPidRequestConverter(pid.ToString()), new SshToSimpleStringConverter()}
            });
        }

        //private readonly  IList<ISshConverter> _converters =new List<ISshConverter>(){ new RunningPidRequestConverter(),new SshToSimpleStringConverter() };

        private class RunningPidRequestConverter : ISshConverter
        {
            private readonly string _pId;

            public RunningPidRequestConverter(string pId)
            {
                _pId = pId;
            }

            public bool CanConvert(Type objectType)
            {
                if (objectType == typeof(bool)) return true;

                return false;
            }

            public object Read(string reader, Type objectType)
            {
                var lines = reader.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length < 2)
                    //var key = NoHupRequest.SynchronizedPIdsCache.TryGetKey(_pId);

                    //if (key != null)
                    //{
                    //    NoHupRequest.SynchronizedPIdsCache.Delete(key);
                    // }
                    //TODO converter error.
                    return false;
                return true;
            }
        }
    }
}