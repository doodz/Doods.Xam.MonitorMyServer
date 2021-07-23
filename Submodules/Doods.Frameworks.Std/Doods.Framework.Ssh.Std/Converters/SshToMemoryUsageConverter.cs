using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Converters
{
    public class SshToMemoryUsageConverter : SshConverter
    {
        private readonly string _keyAvailable = "MemAvailable";
        private readonly string _keyBuffers = "Buffers";
        private readonly string _keyCached = "Cached";
        private readonly string _keyFree = "MemFree";
        private readonly string _keyTotal = "MemTotal";
        private readonly string pattern = @"(.*):\s*(\d*)";

        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OsMemoryBean);
        }

        public override object Read(string content, Type objectType)

        {
            var memoryData = new Dictionary<string, long>();

            foreach (Match m in Regex.Matches(content, pattern, RegexOptions.Multiline))
                memoryData.Add(m.Groups[1].Value, long.Parse(m.Groups[2].Value));


            //var res = content.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            //foreach (var line in res)
            //{
            //    var paragraphs = line.Split(' ');
            //    if (paragraphs.Length > 1)

            //    {

            //        memoryData.Add(paragraphs[0].Replace(':',' '), long.Parse(paragraphs[1]));
            //    }
            //}

            if (memoryData.TryGetValue(_keyTotal, out var memTotal))
            {
                if (memoryData.TryGetValue(_keyAvailable, out var memAvailable))
                    //LOGGER.debug("Using MemAvailable for calculation of free memory.");
                    return new OsMemoryBean(memTotal, memTotal - memAvailable, memoryData);

                // maybe Linux Kernel < 3.14
                // estimate "used": MemTotal - (MemFree + Buffers + Cached)
                // thats also how 'free' is doing it
                var memFree = memoryData.ContainsKey(_keyFree);
                var memCached = memoryData.ContainsKey(_keyCached);
                var memBuffers = memoryData.ContainsKey(_keyBuffers);
                if (memFree && memCached && memBuffers)
                {
                    var memUsed = memTotal - (memoryData[_keyFree] + memoryData[_keyBuffers] + memoryData[_keyCached]);
                    //LOGGER.debug("Using MemFree,Buffers and Cached for calculation of free memory.");
                    return new OsMemoryBean(memTotal, memUsed, memoryData);
                }
            }

            return new OsMemoryBean("error", memoryData);
        }
    }
}