using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace StarCi.CiFarmSDK.Utils
{   
    //abstraction for native http instance
    public partial class RestClient
    {
        //native client
        private HttpClient HttpClient { get; set; }

        public RestClient()
        {
            HttpClient = new HttpClient();
        }
        
        //public void ConfigureHeaders(Dictionary<string, string> headers)
        //{
        //    // Check if the provided dictionary is null or empty
        //    if (headers != null)
        //    {
        //        foreach (var header in headers)
        //        {
        //            // Adding each header to the HttpClient's DefaultRequestHeaders
        //            HttpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        //        }
        //    }
        //    else
        //    {
        //        // Optional: Handle the case where headers are not provided
        //        // You could log or throw an exception if this behavior is desired
        //        System.Console.WriteLine("No headers provided.");
        //    }
        //}

        //whenever call the destroy method, the HttpClient despose
        public void Dispose()
        {
            HttpClient.Dispose();
        }

        //public void Post()
        //{
        //    try
        //    {
        //        var jsonData = JsonConvert.SerializeObject(data);
        //        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        //        using var response = await _httpClient.PostAsync(endpoint, content);
        //        response.EnsureSuccessStatusCode();

        //        var responseBody = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<TResponse>(responseBody);
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        throw new Exception($"Error making POST request to {endpoint}: {ex.Message}", ex);
        //    }
        //}
    }
}