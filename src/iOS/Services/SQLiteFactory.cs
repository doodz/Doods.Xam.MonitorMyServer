using System;
using System.IO;
using Doods.Framework.Repository.Std.Interfaces;

namespace Doods.Xam.MonitorMyServer.iOS.Services
{
    internal class SQLiteFactory : ISqLiteFactory
    {
        public string DefaultDatabaseName { get; } = "MonitorMyServer.db";

        public string GetDatabasePath(string fileName)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

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