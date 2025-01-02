using System.Collections;
using CiFarm.Core.Credentials;
using CiFarm.RestApi;
using CiFarm.Utils;
using UnityEngine;

namespace CiFarm
{
    public enum Environment
    {
        Local,
        Development,
        Staging,
        Production,
    }

    public class CiFarmSDK : SingletonPersistent<CiFarmSDK>
    {
# if CIFARM_SDK_UNITASK_SUPPORT
        [Tooltip("Set the REST API URLs for each environment")]
        [SerializeField]
        private SerializableDictionary<Environment, string> _restApiUrls = new()
        {
            { Environment.Local, "http://localhost:3001" },
            { Environment.Development, "https://api.cifarm.dev.starci.net" },
            { Environment.Staging, "https://api.cifarm.staging.starci.net" },
            { Environment.Production, "https://api.cifarm.starci.net" },
        };

        [Tooltip("Set the environment for using CiFarm SDK")]
        [SerializeField]
        private Environment _environment = Environment.Local;

        private string _restApiUrl;
        private RestApiClient _restApiClient;

        [Tooltip("Set the log scope, useful for debugging")]
        [SerializeField]
        private LogScope _logScope = LogScope.All;

        [Tooltip("Set the rest api version")]
        [SerializeField]
        private RestApiVersion _restApiVersion = RestApiVersion.V1;

        [Tooltip("Set the retry interval")]
        [SerializeField]
        private int _retryInterval = 2000;

        [Tooltip("Set the retry count")]
        [SerializeField]
        private int _retryCount = 2;

        public void Start()
        {
            // Set the log scope
            ConsoleLogger.LogScope = _logScope;

            // Set the environment
            _restApiUrl = _restApiUrls[_environment];
            _restApiClient = new RestApiClient()
            {
                BaseUrl = _restApiUrl,
                ApiVersion = _restApiVersion,
                RetryInterval = _retryInterval,
                RetryCount = _retryCount,
            };

            ConsoleLogger.LogSuccess("CiFarm SDK initialized");
        }

        [Tooltip("Set the credentials for the user")]
        [SerializeField]
        private Credentials _credentials;

        public void SetCredentials(Credentials credentials)
        {
            _credentials = credentials;
        }

        [Header("Editor")]
        [SerializeField]
        private SupportedChainKey _chainKey;

        [SerializeField]
        private Network _network;

        [SerializeField]
        private int _accountNumber;

        public void Authenticate()
        {
            StartCoroutine(AuthenticateCoroutine());
        }

        private IEnumerator AuthenticateCoroutine()
        {
#if UNITY_EDITOR
            AuthenticateAsync();
#else
            yield return new WaitUntil(() => _credentials != null);
            AuthenticateAsync(false);
#endif
            yield return null;
        }

        private async void AuthenticateAsync(bool editor = true)
        {
            if (editor)
            {
                var generateSignatureResponse = await _restApiClient.GenerateSignatureAsync(
                    new()
                    {
                        ChainKey = _chainKey,
                        Network = _network,
                        AccountNumber = _accountNumber,
                    }
                );

                // Set the credentials
                _credentials.ChainKey = generateSignatureResponse.ChainKey;
                _credentials.Message = generateSignatureResponse.Message;
                _credentials.PublicKey = generateSignatureResponse.PublicKey;
                _credentials.Signature = generateSignatureResponse.Signature;
                _credentials.Network = generateSignatureResponse.Network;
                _credentials.TelegramInitDataRaw = generateSignatureResponse.TelegramInitDataRaw;
                _credentials.AccountAddress = generateSignatureResponse.AccountAddress;
            }

            var verifyMessageResponse = await _restApiClient.VerifyMessageAsync(
                new()
                {
                    Message = _credentials.Message,
                    PublicKey = _credentials.PublicKey,
                    Signature = _credentials.Signature,
                    ChainKey = _credentials.ChainKey,
                    Network = _credentials.Network,
                    AccountAddress = _credentials.AccountAddress,
                }
            );

            // Save the authentication token
            AuthToken.Save(verifyMessageResponse.AccessToken, verifyMessageResponse.RefreshToken);
        }
# else
        // Install the UniTask package to enable this feature
#endif
    }
}
