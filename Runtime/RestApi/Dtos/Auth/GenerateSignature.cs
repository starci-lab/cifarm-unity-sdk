#if CIFARM_SDK_JSON_SUPPORT

using System;
using CiFarm.Core.Credentials;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class GenerateSignatureRequest
    {
        [SerializeField]
        private SupportedChainKey _chainKey;

        [SerializeField]
        private Network _network;

        [SerializeField]
        private int _accountNumber;

        [JsonProperty("chainKey")]
        public SupportedChainKey ChainKey
        {
            get => _chainKey;
            set => _chainKey = value;
        }

        [JsonProperty("network")]
        public Network Network
        {
            get => _network;
            set => _network = value;
        }

        [JsonProperty("accountNumber")]
        public int AccountNumber
        {
            get => _accountNumber;
            set => _accountNumber = value;
        }
    }

    [Serializable]
    public class GenerateSignatureResponse
    {
        [SerializeField]
        private SupportedChainKey _chainKey;

        [SerializeField]
        private string _message;

        [SerializeField]
        private string _publicKey;

        [SerializeField]
        private string _signature;

        [SerializeField]
        private Network _network;

        [SerializeField]
        private string _telegramInitDataRaw;

        [SerializeField]
        private string _accountAddress;

        [JsonProperty("chainKey")]
        public SupportedChainKey ChainKey
        {
            get => _chainKey;
            set => _chainKey = value;
        }

        [JsonProperty("message")]
        public string Message
        {
            get => _message;
            set => _message = value;
        }

        [JsonProperty("publicKey")]
        public string PublicKey
        {
            get => _publicKey;
            set => _publicKey = value;
        }

        [JsonProperty("signature")]
        public string Signature
        {
            get => _signature;
            set => _signature = value;
        }

        [JsonProperty("network")]
        public Network Network
        {
            get => _network;
            set => _network = value;
        }

        [JsonProperty("telegramInitDataRaw")]
        public string TelegramInitDataRaw
        {
            get => _telegramInitDataRaw;
            set => _telegramInitDataRaw = value;
        }

        [JsonProperty("accountAddress")]
        public string AccountAddress
        {
            get => _accountAddress;
            set => _accountAddress = value;
        }
    }
}

#endif
