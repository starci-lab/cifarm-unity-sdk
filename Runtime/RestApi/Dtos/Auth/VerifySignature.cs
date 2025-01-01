using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi.Dtos.Auth
{
    // Enum to represent SupportedChainKey (you would replace these values with actual ones from your context)
    public enum SupportedChainKey
    {
        Solana,
        Polkadot,
        // Add other supported chains as necessary
    }

    // Enum to represent Network (you would replace these values with actual ones from your context)
    public enum Network
    {
        Testnet,
        Mainnet,
        // Add other networks as necessary
    }

    [Serializable]
    public class VerifySignatureRequest
    {
        // Backing field for the Message property
        [SerializeField]
        private string _message;

        [JsonProperty("message")]
        public string Message
        {
            get => _message;
            set => _message = value;
        }

        // Backing field for the PublicKey property
        [SerializeField]
        private string _publicKey;

        [JsonProperty("publicKey")]
        public string PublicKey
        {
            get => _publicKey;
            set => _publicKey = value;
        }

        // Backing field for the Signature property
        [SerializeField]
        private string _signature;

        [JsonProperty("signature")]
        public string Signature
        {
            get => _signature;
            set => _signature = value;
        }

        // Backing field for the ChainKey property
        [SerializeField]
        private SupportedChainKey? _chainKey;

        [JsonProperty("chainKey")]
        public SupportedChainKey? ChainKey
        {
            get => _chainKey;
            set => _chainKey = value;
        }

        // Backing field for the Network property
        [SerializeField]
        private Network? _network;

        [JsonProperty("network")]
        public Network? Network
        {
            get => _network;
            set => _network = value;
        }

        // Backing field for the AccountAddress property
        [SerializeField]
        private string _accountAddress;

        [JsonProperty("accountAddress")]
        public string AccountAddress
        {
            get => _accountAddress;
            set => _accountAddress = value;
        }
    }

    // Model to represent the VerifySignatureResponse
    [Serializable]
    public class VerifySignatureResponse
    {
        // Backing field for the AccessToken property
        [SerializeField]
        private string _accessToken;

        [JsonProperty("accessToken")]
        public string AccessToken
        {
            get => _accessToken;
            set => _accessToken = value;
        }

        // Backing field for the RefreshToken property
        [SerializeField]
        private string _refreshToken;

        [JsonProperty("refreshToken")]
        public string RefreshToken
        {
            get => _refreshToken;
            set => _refreshToken = value;
        }
    }
}
