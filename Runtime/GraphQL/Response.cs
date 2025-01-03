using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace CiFarm.GraphQL
{
    [Serializable]
    // Empty class, use for null inhenrit from generic class
    public class GraphQLResponse<TData>
        where TData : class, new()
    {
        [SerializeField]
        private JObject _data;

        [JsonProperty("data")]
        public JObject Data
        {
            get => _data;
            set => _data = value;
        }

        [JsonProperty("_")]
        //name of the query
        public string Name { get; set; }

        public TData GetData()
        {
            return _data[Name].ToObject<TData>();
        }
    }
}
