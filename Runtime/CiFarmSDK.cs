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
                RetryInterval = _retryCount,
                RetryCount = _retryInterval,
            };

            ConsoleLogger.LogSuccess("CiFarm SDK initialized");
        }

# else
        // Install the UniTask package to enable this feature
#endif
    }
}
