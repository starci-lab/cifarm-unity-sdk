using System.Threading.Tasks;

namespace StarCi.CiFarmSDK.Services
{
    public class GraphqlApiService : IGraphqlApiService
    {
        public async Task<string> List1(string query = "")
        {
            if (string.IsNullOrEmpty(query))
            {
                return "";
            }
            else
            {
                return "";
            }
        }
    }
}
