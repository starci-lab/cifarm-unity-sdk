using StarCi.CiFarmSDK.Utils;
using UnityEngine;

//[Icon("Runtime/Logo.png")]
public class CiFarmSDK : SingletonPersistent<CiFarmSDK>
{
    [Tooltip("Enter the URL for the REST API endpoint")]
    [SerializeField]
    private string restApiUrl = "";

    [Tooltip("Enter the URL for the REST API endpoint")]
    [SerializeField]
    private string graphQlUrl = "";

    [Tooltip("Enter the URL for the REST API endpoint")]
    [SerializeField]
    private string websocket = "";

    [SerializeField]
    private bool enableCache;

    [SerializeField]
    private bool cors;
    [HideInInspector]
    public RestClient RestClient { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //create client
        RestClient = new ();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        //close client
        RestClient.Dispose();
    }
}
