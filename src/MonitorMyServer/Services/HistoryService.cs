using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Doods.Framework.Http.Std.Interfaces;
using Doods.Framework.Http.Std.Serializers;
using Doods.Xam.MonitorMyServer.Data;
using Xamarin.Essentials;

namespace Doods.Xam.MonitorMyServer.Services
{
    public class HistoryService
    {
        private readonly NewtonsoftJsonSerializer _jsonSerializer = new NewtonsoftJsonSerializer();
       
        public Task SetHistoryAsync(int id,History history)
        {
            var cacheDir = FileSystem.CacheDirectory;

            var templateFileName = Path.Combine(cacheDir, id + "_history.json");

            using (var stream = File.OpenWrite(templateFileName))
            {
                using (var writer = new StreamWriter(stream))
                {
                  var jsonHistory  =_jsonSerializer.Serialize(history);

                  return writer.WriteAsync(jsonHistory);
                  
                }
            }


        }

        public Task<History> GetHistoryAsync(int id)
        {
            var cacheDir = FileSystem.CacheDirectory;

            var templateFileName = Path.Combine(cacheDir, id + "_history.json");

            using (var stream = File.OpenRead(templateFileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    var fileContents = reader.ReadToEndAsync();

                    return fileContents.ContinueWith(task => _jsonSerializer.Deserialize<History>(task.Result));

                  
                }
            }


        }
    }
}
