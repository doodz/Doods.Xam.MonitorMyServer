using System;
using Doods.Framework.Http.Std.Serializers;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Openmediavault.Rpc.Std.Enums;
using Doods.Openmedivault.Ssh.Std.Requests;
using Newtonsoft.Json;
using RestSharp;

namespace Doods.Openmediavault.Rpc.Std
{
    public interface IRpcRequest
    {
        string Service { get; set; }
        string Method { get; set; }
        object Params { get; set; }
        Options Options { get; set; }
    }

    public class RpcRequest : IRpcRequest
    {
        [JsonProperty("service")] public string Service { get; set; }

        [JsonProperty("method")] public string Method { get; set; }

        [JsonProperty("params")] public object Params { get; set; }

        [JsonProperty("options")] public Options Options { get; set; }
    }


    public class ParamsListFilter
    {
        [JsonProperty("start")] public int Start { get; set; } = 0;

        [JsonProperty("limit")] public int Limit { get; set; } = 25;

        public ParamsListFilter()
        {
        }

        public ParamsListFilter(int start, int limit)
        {
            Start = start;
            Limit = limit;
        }
    }

    public class Options
    {
        [JsonProperty("updatelastaccess")] public bool Updatelastaccess { get; set; }
    }


    public class Requestbuilder
    {
        private static string Ssh = "omv-rpc";

        private static string Http = "rpc.php";
        private static string Rrd ="rrd.php";
        //public T Build<T>(RpcRequest request,RequestType type)
        //{
        //    switch (type)
        //    {
        //        case RequestType.ssh:
        //            return (object)ToSsh(request);
        //            break;
        //        case RequestType.http:
        //            return (object)ToHttp(request);
        //            break;

        //    }

        //    return default;
        //}
        OmvSerializer serializer = new OmvSerializer();


        public static string ToJson(object obj) => JsonConvert.SerializeObject(obj, OmvSerializer.Settings);

        public RestRequest ToGetRrd(string filename)
        {
            var getrrd = new LocalRestRequest(Rrd, Method.GET);
            getrrd.AddParameter("name", Uri.EscapeDataString(filename));

            return getrrd;
        }


        public RestRequest ToHttp(IRpcRequest request)
        {
            var toto = new LocalRestRequest(Http, Method.POST);
            toto.AddJsonBody(request);

            return toto;
        }

        public ISshRequest ToSsh(IRpcRequest request)
        {
            if (request.Params != null)
                return new DefaultSshRequest($"{Ssh} {request.Service} {request.Method} \"{ToJson(request.Params).Replace("\"","\\\"")}\"");

            return new DefaultSshRequest($"{Ssh} {request.Service} {request.Method}");
        }
    }

    /// <summary>
    /// todo HttpRequest  Doods.Framework.Http.Std
    /// </summary>
    internal class LocalRestRequest : RestRequest
    {
        public LocalRestRequest(string resource, Method method) : base(resource, method, DataFormat.Json)
        {
            JsonSerializer = new NewtonsoftJsonSerializer(LocalJsonConverter.Singleton);
        }
    }

    internal class DefaultSshRequest : OmvRequestBase
    {
        public DefaultSshRequest(string requestString) : base(requestString)
        {
        }
    }
}