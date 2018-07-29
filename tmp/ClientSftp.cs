using Doods.Framework.Ssh.Std.Beans;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Doods.Framework.Ssh.Std
{
    public class ClientSftp
    {
        private readonly string _host;
        private readonly string _username;
        private readonly string _password;
        private readonly Lazy<SftpClient> _client;

        public ClientSftp(string host, string username, string password)
        {
            _host = host;
            _username = username;
            _password = password;
            _client = new Lazy<SftpClient>(() => new SftpClient(_host, _username, _password));
        }

        public async Task GetFiles(IEnumerable<string> files, string localFolder)
        {
            foreach (var file in files)
                await GetFile(file, Path.Combine(localFolder, file));
        }

        public async Task GetFiles(IEnumerable<FileInfoBean> files, string localFolder)
        {
            foreach (var file in files)
                await GetFile(file, Path.Combine(localFolder, file.Name));
        }

        public async Task<Stream> GetFile(FileInfoBean file, string localPath)
        {
            return await GetFile(Path.Combine(file.Path, file.Name), localPath);
        }

        public async Task<Stream> GetFile(string filePath, string localPath)
        {
            if (!_client.Value.IsConnected) _client.Value.Connect();
            using (var saveFile = File.OpenWrite(localPath))
            {
                Func<IAsyncResult, Stream> endMethod = (a) =>
                {
                    _client.Value.EndDownloadFile(a);
                    return null;
                };
                return await Task.Factory.FromAsync(_client.Value.BeginDownloadFile(filePath, saveFile), endMethod);
            }
        }


        //public async Task<Stream> GetFile(string filePath, IFile localFile)
        //{
        //    if (!_client.Value.IsConnected) _client.Value.Connect();
        //    using (var saveFile = await localFile.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
        //    {
        //        Func<IAsyncResult, Stream> endMethod = (a) =>
        //        {
        //            _client.Value.EndDownloadFile(a);
        //            return saveFile;
        //        };






        //        return await Task.Factory.FromAsync(_client.Value.BeginDownloadFile(filePath, saveFile), endMethod);
        //    }
        //}

        public async Task GetFiles(IEnumerable<SftpFile> files, string localFolder)
        {
            foreach (var file in files)
                await GetFile(file, localFolder);
        }

        public async Task<Stream> GetFile(SftpFile file, string localPath)
        {
            return await GetFile(file.FullName, localPath);
        }

        public async Task<IEnumerable<SftpFile>> GetAllFileFromPath(string remoteDirectory)
        {
            using (var sftp = new SftpClient(_host, _username, _password))
            {
                sftp.Connect();

                var res = await Task.Factory.FromAsync(
                    (asyncCallback, state) => sftp.BeginListDirectory(remoteDirectory, asyncCallback, state),
                    sftp.EndListDirectory, null);

                return res.Where(f => !f.Name.StartsWith("."));
            }
        }

        //private void Toto(string remoteDirectory, string localFileName)
        //{
        //    using (var sftp = new SftpClient(_host, _username, _password))
        //    {
        //        sftp.Connect();
        //        var files = sftp.ListDirectory(remoteDirectory);
        //        foreach (var file in files)
        //        {
        //            if (!file.Name.StartsWith("."))
        //            {

        //                File.OpenWrite(localFileName);
        //                var stream = File.OpenRead(remoteDirectory + file.Name);
        //                sftp.DownloadFile(remoteDirectory, stream);
        //            }
        //        }
        //    }
        //}
    }
}