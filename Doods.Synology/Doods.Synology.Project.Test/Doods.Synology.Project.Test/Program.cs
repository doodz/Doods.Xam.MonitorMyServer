﻿using System;
using System.IO;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Synology.Webapi.Std;

namespace Doods.Synology.Project.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("Hello World!");
          

            var result = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var file = File.ReadAllText(result+ "\\pass\\doc3.txt").Trim();
            var sp = file.Split(';');
            var con = GetConnection(sp);
            var client = new SynologyApi( con);
            // client.SetHandlers(new NewtonsoftJsonSerializer(LocalJsonConverter.Singleton));
            var auth = new SynoAuthClient(client);
            if (await auth.LoginAsync(sp[0],sp[1]))
            {
                var desck = new SynoDesktopClient(client);
                var services = await desck.GetUserServices();


                var local = new SynoFileStationClient(client);
                var GetSharedFolders = await local.GetSharedFolders();
                var GetFileStationList = await local.GetFileStationList();
                //var systemInfo = client.GetSystemInfo().GetAwaiter().GetResult();
                //var networkInfo = client.GetNetworkInfo().GetAwaiter().GetResult();

            }

            Console.ReadKey();

        }

        private static IConnection GetConnection(string[] split)
        {
            return new HttpConnection(split[2], int.Parse(split[3]));
        }
    }
}
