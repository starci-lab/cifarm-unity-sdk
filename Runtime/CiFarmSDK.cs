using System.Collections;
using CiFarm.Core.Credentials;
using CiFarm.RestApi;
using CiFarm.Utils;

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
        [UnityEngine.Tooltip("Set the REST API URLs for each environment")]
        [UnityEngine.SerializeField]
        private SerializableDictionary<Environment, string> _restApiUrls = new()
        {
            { Environment.Local, "http://localhost:3001" },
            { Environment.Development, "https://api.cifarm.dev.starci.net" },
            { Environment.Staging, "https://api.cifarm.staging.starci.net" },
            { Environment.Production, "https://api.cifarm.starci.net" },
        };

        [UnityEngine.Tooltip("Set the environment for using CiFarm SDK")]
        [UnityEngine.SerializeField]
        private Environment _environment = Environment.Local;

        private string _restApiUrl;
        private RestApiClient _restApiClient;

        [UnityEngine.Tooltip("Set the log scope, useful for debugging")]
        [UnityEngine.SerializeField]
        private LogScope _logScope = LogScope.All;

        [UnityEngine.Tooltip("Set the rest api version")]
        [UnityEngine.SerializeField]
        private RestApiVersion _restApiVersion = RestApiVersion.V1;

        [UnityEngine.Tooltip("Set the retry interval")]
        [UnityEngine.SerializeField]
        private int _retryInterval = 2000;

        [UnityEngine.Tooltip("Set the retry count")]
        [UnityEngine.SerializeField]
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

        [UnityEngine.Tooltip("Set the credentials for the user")]
        [UnityEngine.SerializeField]
        private Credentials _credentials;

        public void SetCredentials(Credentials credentials)
        {
            _credentials = credentials;
        }

        [UnityEngine.Header("Editor")]
        [UnityEngine.SerializeField]
        private SupportedChainKey _chainKey;

        [UnityEngine.SerializeField]
        private Network _network;

        [UnityEngine.SerializeField]
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
