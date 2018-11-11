using System.IO;
using Windows.Storage;
using Doods.Framework.Mobile.Std.Interfaces;

namespace Doods.Xam.MonitorMyServer.UWP.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filePath)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filePath);
        }

        public string GetLocalDirectoryPath()
        {
            return ApplicationData.Current.LocalFolder.Path;
        }
    }
}