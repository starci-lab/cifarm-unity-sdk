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

            // Debug or process the response
            //Debug.Log($"GraphQL GET Response: {JsonConvert.SerializeObject(response, Formatting.Indented)}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error making GraphQL GET request: {ex.Message}");
        }
    }
}
