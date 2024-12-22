using NUnit.Framework;

namespace StarCi.CiFarmSDK.Services
{
    public class GraphqlApiService : IGraphqlApiService
    {
        public async string List1(string query = "")
        {
            if (string.IsNullOrEmpty(query))
            {

            }   else
            {
                return "";
            } 
        }
    }
}
