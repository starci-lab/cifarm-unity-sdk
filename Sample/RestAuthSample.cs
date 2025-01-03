using System.Collections;
using CiFarm;
using Codice.Client.Commands;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Sample
{
    public class RestAuthSample : MonoBehaviour
    {
        public IEnumerator Start()
        {
            yield return new WaitUntil(() => CiFarmSDK.Instance != null);
            CiFarmSDK.Instance.Authenticate();
            var x = CiFarmSDK.Instance.GraphQLClient.QueryActivitiesAsync();
            yield return new WaitUntil(() => x.Status == UniTaskStatus.Succeeded);

            Debug.Log(JsonConvert.SerializeObject(x.AsValueTask().Result));
        }
    }
}
