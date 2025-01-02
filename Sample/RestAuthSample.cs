using CiFarm;
using UnityEngine;

namespace CiFarm.Sample
{
    public class RestAuthSample : MonoBehaviour
    {
        public void Start()
        {
            CiFarmSDK.Instance.Authenticate();
        }
    }
}
