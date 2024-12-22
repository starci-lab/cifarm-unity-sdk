using StarCi.CiFarmSDK.Configs;
using StarCi.CiFarmSDK.Services;
using StarCi.CiFarmSDK.Types.Gameplay.Auth;
using StarCi.CiFarmSDK.Types.Gameplay.Shop;
using StarCi.CiFarmSDK.Utils;
using UnityEngine;

public class BuySeedExample : MonoBehaviour
{
    private async void Start()
    {
        // Singleton pattern
        IRestApiHttpClient apiHttpClient = RestApiHttpClient.Instance;

        // Configure the client (without tokens initially)
        apiHttpClient.ConfigureClient(BaseConfigs.BaseUrls.Rest, headers =>
        {
            headers.Add("Accept", "application/json");
        });

        // Initialize the RestApiService
        RestApiService restApiService = new RestApiService(apiHttpClient);

        try
        {
            // Step 1: Call TestSignature API
            var testSignatureRequest = new TestSignatureRequest
            {
                ChainKey = "avalanche",
                AccountNumber = 0,
                Network = "testnet"
            };

            var testSignatureResponse = await restApiService.TestSignature(testSignatureRequest);
            if (testSignatureResponse == null)
            {
                Debug.LogError("Failed to call TestSignature API.");
                return;
            }

            Debug.Log($"TestSignature Response: {testSignatureResponse}");

            // Step 2: Call VerifySignature API
            var verifySignatureRequest = new VerifySignatureRequest
            {
                ChainKey = testSignatureResponse.ChainKey,
                Message = testSignatureResponse.Message,
                PublicKey = testSignatureResponse.PublicKey,
                Signature = testSignatureResponse.Signature,
                Network = testSignatureResponse.Network,
                AccountAddress = testSignatureResponse.AccountAddress
            };


            var verifySignatureResponse = await restApiService.VerifySignature(verifySignatureRequest);
            if (verifySignatureResponse == null)
            {
                Debug.LogError("Failed to call VerifySignature API.");
                return;
            }

            Debug.Log($"VerifySignature Response: AccessToken={verifySignatureResponse.AccessToken}, RefreshToken={verifySignatureResponse.RefreshToken}");

            // Step 3: Update the API client with tokens
            apiHttpClient.ConfigureClient(BaseConfigs.BaseUrls.Rest, headers =>
            {
                headers.Add("Authorization", $"Bearer {verifySignatureResponse.AccessToken}");
                headers.Add("Accept", "application/json");
            });

            // Step 4: Call BuySeeds API
            var buySeedsRequest = new BuySeedsRequest
            {
                CropId = "carrot",
                Quantity = 10
            };

            var buySeedsResponse = await restApiService.BuySeeds(buySeedsRequest);
            if (buySeedsResponse != null)
            {
                Debug.Log("BuySeeds API called successfully!");
            }
            else
            {
                Debug.LogError("Failed to call BuySeeds API.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"An error occurred: {ex.Message}");
        }
    }

}