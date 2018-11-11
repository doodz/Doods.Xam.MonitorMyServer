using System;
using System.IO;
using Doods.Framework.Mobile.Std.Interfaces;

namespace Doods.Xam.MonitorMyServer.Droid.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filePath)
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filePath);
        }

        public string GetLocalDirectoryPath()
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            return directory;
        }
    }
}