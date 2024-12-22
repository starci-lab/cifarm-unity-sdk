using StarCi.CiFarmSDK.Configs;
using StarCi.CiFarmSDK.Utils;
using System;
using UnityEngine;

public class GraphqlGetToolsExample : MonoBehaviour
{
    private async void Start()
    {
        //Singleton pattern
        IRestApiHttpClient apiHttpClient = RestApiHttpClient.Instance;

        //Configure the client(optional)
        apiHttpClient.ConfigureClient(BaseConfigs.BaseUrls.Graphql, headers =>
        {
            headers.Add("Authorization", "Bearer your-token");
            headers.Add("Accept", "application/json");
        });

        const string graphqlEndpoint = "/";
        const string query = @"
        {
            tools(args: { offset: 0, limit: 10 }) {
            id
            index
            availableIn
            createdAt
            updatedAt
            }
        }";

        try
        {
            // URL encode the query for GET
            string encodedQuery = Uri.EscapeDataString(query);
            string queryParams = $"query={encodedQuery}";

            // Execute GET request
            //var response = await apiHttpClient.GetAsync<object>(graphqlEndpoint, queryParams);

<<<<<<< HEAD
            //// Debug or process the response
=======
            // Debug or process the response
>>>>>>> 4e6ac25c8bb6f7917c10fa5c8b47885923b9bf10
            //Debug.Log($"GraphQL GET Response: {JsonConvert.SerializeObject(response, Formatting.Indented)}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error making GraphQL GET request: {ex.Message}");
        }
    }
}
