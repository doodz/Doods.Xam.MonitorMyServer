using System;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Synology.Webapi.Std;

namespace Doods.Synology.Project.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");
            var con = GetConnection();

            var result = Environment.GetEnvironmentVariable(Environment.SpecialFolder.Personal);

            var sp =result.Split(';');

            var client = new SynologyCgiService(null, con);
           // client.SetHandlers(new NewtonsoftJsonSerializer(LocalJsonConverter.Singleton));

            if (client.LoginAsync(sp[0],sp[1]).GetAwaiter().GetResult())
            {
              


                var systemInfo = client.GetSystemInfo().GetAwaiter().GetResult();
                var networkInfo = client.GetNetworkInfo().GetAwaiter().GetResult();

            }

            Console.ReadKey();

        }

        private static IConnection GetConnection()
        {
            return new HttpConnection("https://petitboudin/webapi", 5001);
        }
    }
}
