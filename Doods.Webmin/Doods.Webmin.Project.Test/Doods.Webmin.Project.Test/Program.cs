using System;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Webmin.Webapi.Std;
using Doods.Webmin.Webapi.Std.Clients;

namespace Doods.Webmin.Project.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");
            var con = GetConnection();
            var client = new WebminApi(con);

            var login = new WebminloginClient(client);
           // client.SetHandlers(new NewtonsoftJsonSerializer(LocalJsonConverter.Singleton));
            if (login.LoginAsync("root", "root").GetAwaiter().GetResult())
            {
              
                login.LogOut();

                //var systemInfo = client.GetSystemInfo().GetAwaiter().GetResult();
                //var networkInfo = client.GetNetworkInfo().GetAwaiter().GetResult();

            }

            Console.ReadKey();

        }

        private static IConnection GetConnection()
        {
            return new HttpConnection("https://openmediavault5", 10000);
        }
    }
}
