using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Psbds.LUIS.IntentCSVImport.Core
{
    public class LuisClient
    {
        private const string BASE_URL = "https://westus.api.cognitive.microsoft.com";
        private const string WEB_API_URL = "https://westus.api.cognitive.microsoft.com/luis/webapi/v2.0/apps";
        private const string API_URL = "https://westus.api.cognitive.microsoft.com/luis/api/v2.0/apps";

        private readonly string _applicationKey;

        public LuisClient(string applicationKey)
        {
            _applicationKey = applicationKey;
        }

        public async Task<String> ExportVersion(string applicationId, string applicationVersion)
           => await SendGetRequest($"{API_URL}/{applicationId}/versions/{applicationVersion}/export");



        private async Task<String> SendGetRequest(string path)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", this._applicationKey);
                httpClient.BaseAddress = new Uri(BASE_URL);
                var result = await httpClient.GetAsync(path);
                var content = await result.Content.ReadAsStringAsync();
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception(content);
                }
                return content;
            }
        }
    }
}
