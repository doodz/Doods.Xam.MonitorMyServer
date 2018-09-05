using Doods.Framework.Repository.Std.Interfaces;
using System;
using System.IO;

namespace Doods.Xam.MonitorMyServer.Droid.Services
{
    internal class SQLiteFactory : ISqLiteFactory
    {
        public string DefaultDatabaseName { get; } = "MonitorMyServer.db";

        public string GetDatabasePath(string fileName)
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));

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

