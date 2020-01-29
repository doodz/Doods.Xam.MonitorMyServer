using System;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Http.Std.Serializers;
using Doods.Openmediavault.Rpc.Std;
using Doods.Openmedivault.Http.Std;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmediavault.Project.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var con = GetConnection();
            var client = new OmvHttpService(null, con);
            client.SetHandlers(new NewtonsoftJsonSerializer(LocalJsonConverter.Singleton));
            if (client.LoginAsync("admin", "admin").GetAwaiter().GetResult())
            {
                var objTest = new OmvRpcService(client);

                var result = objTest.GetSystemInformations().GetAwaiter().GetResult();
            }

            Console.ReadKey();
        }


        private static IConnection GetConnection()
        {
            return new HttpConnection("https://192.168.1.40", 80);
        }
    }
}