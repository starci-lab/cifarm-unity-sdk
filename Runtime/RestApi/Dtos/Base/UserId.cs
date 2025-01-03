#if CIFARM_SDK_JSON_SUPPORT

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class NeighborAndUserIdRequest
    {
        [SerializeField]
        private string _neighborUserId;

        [JsonProperty("neighborUserId")]
        public string NeighborUserId
        {
            get => _neighborUserId;
            set => _neighborUserId = value;
        }
    }
}

#endif
