using StarCi.CiFarmSDK.Configs;
using StarCi.CiFarmSDK.Services;
using StarCi.CiFarmSDK.Types.Gameplay.Auth;
using StarCi.CiFarmSDK.Utils;
using UnityEngine;

public class HelloMessage : MonoBehaviour
{
    private async void Start()
    {
        //Singleton pattern
        IRestApiHttpClient apiHttpClient = RestApiHttpClient.Instance;

        //Configure the client(optional)
        apiHttpClient.ConfigureClient(BaseConfigs.BaseUrls.Rest, headers =>
        {
            headers.Add("Authorization", "Bearer your-token");
            headers.Add("Accept", "application/json");
        });

        RestApiService restApiService = new RestApiService(apiHttpClient);

        var request = new MessageRequest
        {
        };

        var response = await restApiService.Message(request);

        if (response != null)
        {
            Debug.Log($"Message Response: {response.Message}");
        }
        else
        {
            Debug.LogError("Failed to call Message API.");
        }
    }
}
