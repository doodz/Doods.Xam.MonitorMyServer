using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Doods.Framework.Http.Std.Interfaces
{
    public interface IJsonSerializer : ISerializer, IDeserializer
    {
    }
}