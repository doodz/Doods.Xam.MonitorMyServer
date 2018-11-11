using System;
using System.IO;
using Windows.Storage;
using Doods.Framework.Repository.Std.Interfaces;

namespace Doods.Xam.MonitorMyServer.UWP.Services
{
    internal class SQLiteFactory : ISqLiteFactory
    {
        public string DefaultDatabaseName { get; } = "MonitorMyServer.db";

        public string GetDatabasePath(string fileName)
        {
            var directory = ApplicationData.Current.LocalFolder.Path;

            if (!Directory.Exists(directory))
                try
                {
                    Directory.CreateDirectory(directory);
                }
                catch (Exception e)
                {
                    var msg = e.Message;
                }

            return Path.Combine(directory, fileName);
        }
    }
}