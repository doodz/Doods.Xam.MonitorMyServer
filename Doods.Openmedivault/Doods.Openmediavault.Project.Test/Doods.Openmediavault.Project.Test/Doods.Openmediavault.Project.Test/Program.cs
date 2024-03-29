﻿using System;
using System.IO;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Http.Std.Serializers;
using Doods.Openmediavault.Rpc.Std;
using Doods.Openmediavault.Rpc.Std.Clients;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Openmediavault.Rpc.Std.Data.V4.FileSystem;
using Doods.Openmediavault.Rpc.Std.Seruializer;
using Doods.Openmedivault.Http.Std;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmediavault.Project.Test
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var resultstr = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var file = File.ReadAllText(resultstr + "\\pass\\doc2.txt").Trim();
            var sp = file.Split(';');
            var con = GetConnection(sp);
            var client = new OmvHttpService(null, con);
            client.SetHandlers(new NewtonsoftJsonSerializer(LocalJsonConverter.Singleton));
            if (await client.LoginAsync(sp[0], sp[1]))
            {
                var objTest = new OmvSystemClient(client);

                var result = await objTest.GetSystemInformations();
                
                var filesystem = new OmvFileSystemMgmtClient(client);
                var resultfile = await filesystem.GetFilesystems();
                var omvExecClient = new OmvExecClient(client);
                var GetCandidatesBgstring = await filesystem.GetCandidatesBg();
                var output = new Output<string>();
                do
                {
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    output = await omvExecClient.GetOutput<string>(GetCandidatesBgstring,0);
                    
                } while (output.Running);


            }

            Console.ReadKey();
        }


        private static IConnection GetConnection(string[] split)
        {
            return new HttpConnection(split[2], int.Parse(split[3]));
        }
    }
}