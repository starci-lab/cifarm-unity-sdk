using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi.Dtos.Auth
{
    [Serializable]
    public class RequestMessageRequest { }

    [Serializable]
    public class RequestMessageResponse
    {
        [SerializeField]
        private string _message;

        [JsonProperty("message")]
        public string Message
        {
            get => _message;
            set => _message = value;
        }
    }
}
