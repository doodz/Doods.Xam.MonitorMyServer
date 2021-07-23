using System;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Serializers
{
    public interface ISshSerializer : IDeserializer
    {
        T Deserialize<T>(IApiResponse apiResponse);

       T DeserializeError<T>(IApiResponse apiResponse);

        T Deserialize<T>(string content);
    }

    public class SshSerializer : ISshSerializer
    {
        private readonly DoodsSshRequestSerializer _serializer;


        public SshSerializer()
        {
            _serializer = new DoodsSshRequestSerializer();
        }

        public SshSerializer(ISshSerializerSettings settings)
        {
            _serializer = DoodsSshRequestSerializer.CreateDefault(settings);
        }

        public T Deserialize<T>(IApiResponse apiResponse)
        {
            return Deserialize<T>(apiResponse.Content);
        }

        public T DeserializeError<T>(IApiResponse apiResponse)
        {
            return Deserialize<T>(apiResponse.ErrorMessage);
        }

        public T Deserialize<T>(string content)
        {
            try
            {
                return _serializer.Deserialize<T>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Logger.Error($"Couldn't deserialize json: {json}. Error: {ex}");
                throw;
            }
        }
    }
}