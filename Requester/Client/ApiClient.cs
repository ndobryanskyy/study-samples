using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;

namespace Requester.Client
{
    public class ApiClient
    {
        public async Task<HttpResponseMessage> PostString(string endpoint, string data)
        {
            return await endpoint
                .PostStringAsync(data);
        }
    }
}