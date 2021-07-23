using System;
using System.Collections.Generic;
using System.Linq;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class GetListFileBaseQuery : GenericQuery<List<FileInfoBean>>
    {
        private readonly string _path;
        private readonly string _query;

        public GetListFileBaseQuery(IClientSsh client, string path) : base(client)
        {
            _path = path;
            _query = $"ls -l {_path} --time-style=long-iso";
            CmdString = _query;
        }

        protected override List<FileInfoBean> PaseResult(string result)
        {
            var lst = new List<FileInfoBean>();


            var lines = result.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);


            foreach (var line in lines.Skip(1)) // remove "total xxxx"
            {
                var split = line.Trim().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var fileInfo = new FileInfoBean();

                fileInfo.Path = _path;
                fileInfo.AccessRights = split[0];
                fileInfo.IsFolder = fileInfo.AccessRights.StartsWith("d");
                fileInfo.Id = int.Parse(split[1]);
                fileInfo.Owner = split[2];
                fileInfo.Group = split[3];
                fileInfo.Size = long.Parse(split[4]);
                fileInfo.Date = DateTime.Parse(split[5]);
                fileInfo.Hour = split[6];
                fileInfo.Name = split[7];

                lst.Add(fileInfo);
            }

            return lst;
        }
    }
}