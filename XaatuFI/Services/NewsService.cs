using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CryptoMarketTracker.Services
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/v2/");
        }

        public async Task<JArray> GetCryptoNewsAsync()
        {
            // Keep hitting the rate limit while adjusting CSS, delay seems to be helping along with reducing the number of cryptos shown (inside CryptoService)
            await Task.Delay(1000); 

            var response = await _httpClient.GetStringAsync("news/?lang=EN");
            var json = JObject.Parse(response);
            return json["Data"] as JArray;
        }
    }
}