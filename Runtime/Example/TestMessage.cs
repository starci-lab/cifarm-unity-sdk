using StarCi.CiFarmSDK.Types.Gameplay.Auth;
using UnityEngine;

public class TestMessage : MonoBehaviour
{
    private async void Start()
    {
        try
        {
            var request = new MessageRequest
            {
            };

            var response = await CiFarmSDK.Instance.RestClient.Message(request);
            if (response == null)
            {
                Debug.LogError("Failed to call Message API.");
                return;
            }

            Debug.Log($"Message Response: {response}");

        }
        catch (System.Exception ex)
        {
            Debug.LogError($"An error occurred: {ex.Message}");
        }
    }

}