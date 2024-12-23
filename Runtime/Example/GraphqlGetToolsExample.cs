using System;
using UnityEngine;

public class GraphqlGetToolsExample : MonoBehaviour
{
    private async void Start()
    {
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

            //Debug.Log($"GraphQL GET Response: {JsonConvert.SerializeObject(response, Formatting.Indented)}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error making GraphQL GET request: {ex.Message}");
        }
    }
}
