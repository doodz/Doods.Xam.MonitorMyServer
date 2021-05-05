using System;
using System.IO;
using System.Threading.Tasks;
using Doods.Framework.Http.Std.Serializers;
using Doods.Xam.MonitorMyServer.Data;
using Xamarin.Essentials;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface IHistoryService
    {
        History CurrentHistoryItem { get; }
        Task SetHistoryAsync(long id, History history);
        Task UpdateLastLoginAsync(long id);
        Task<History> GetHistoryAsync(long id, bool b = false);
    }

    public class HistoryService : IHistoryService
    {
        private readonly NewtonsoftJsonSerializer _jsonSerializer = new NewtonsoftJsonSerializer();

        public History CurrentHistoryItem { get; private set; }

        public Task SetHistoryAsync(long id, History history)
        {
            //var cacheDir = FileSystem.AppDataDirectory;

            //var templateFileName = Path.Combine(cacheDir, id + "_history.json");

            //using (var stream = File.OpenWrite(templateFileName))
            //{
            //    using (var writer = new StreamWriter(stream))
            //    {
            //        var jsonHistory = _jsonSerializer.Serialize(history);

            //        return writer.WriteAsync(jsonHistory);
            //    }
            //}
            return Task.FromResult(0);
        }

        public async Task UpdateLastLoginAsync(long id)
        {
            var history = await GetHistoryAsync(id, true).ConfigureAwait(false);
            history.LastSync = DateTime.Now;
            history.HostId = id;
            await SetHistoryAsync(id, history).ConfigureAwait(false);
        }

        public async Task<History> GetHistoryAsync(long id, bool b = false)
        {
            try
            {
                var cacheDir = FileSystem.AppDataDirectory;

                var templateFileName = Path.Combine(cacheDir, id + "_history.json");

                if (!File.Exists(templateFileName))
                    return new History();

                using (var stream = File.OpenRead(templateFileName))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var fileContents = await reader.ReadToEndAsync();
                        //var tmp = _jsonSerializer.Deserialize<History>(fileContents);
                        History tmp = null;
                        if (b)
                            CurrentHistoryItem = tmp;

                        return tmp??new History();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new History();
        }
    }
}