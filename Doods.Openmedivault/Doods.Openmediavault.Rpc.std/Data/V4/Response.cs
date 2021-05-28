using System.Collections;
using System.Collections.Generic;
using Doods.Openmediavault.Rpc.Std.Interfaces;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Data.V4
{
    public class ResponseError<T> where T : IOmvObject
    {
        [JsonProperty("response")] public string Response { get; set; }

        [JsonProperty("error")] public T Error { get; set; }
    }

    public class ResultList<T> where T : IOmvObject
    {
        [JsonProperty("response")] public T[] Response { get; set; }

        [JsonProperty("error")] public object Error { get; set; }
    }


    public class ResponseList<T> : IEnumerableResponse, IEnumerable<T> where T : IOmvObject
    {
        [JsonProperty("total")] public long Total { get; set; }

        [JsonProperty("data")] public List<T> Data { get; set; }

        [JsonIgnore]
        public T this[int index]
        {
            get => Data[index];
            set => Data.Insert(index, value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public class ResponseArray<T> : IEnumerableResponse where T : IOmvObject
    {
        [JsonProperty("total")] public long Total { get; set; }

        [JsonProperty("data")] public T[] Data { get; set; }
    }
}