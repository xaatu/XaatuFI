using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CryptoMarketTracker.Models;

namespace CryptoMarketTracker.Services
{
    public class CryptoService
    {
        private readonly HttpClient _httpClient;

        public CryptoService(HttpClient httpClient)
{
    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
    _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("CryptoMarketTracker/1.0");
}

        public async Task<List<CryptoCurrency>> GetTopCryptosAsync(int limit = 10, string currency = "usd")
        {
            var response = await _httpClient.GetStringAsync($"coins/markets?vs_currency={currency}&order=market_cap_desc&per_page={limit}&page=1&sparkline=false");
            var jsonArray = JArray.Parse(response);
            var cryptoList = new List<CryptoCurrency>();

            foreach (var item in jsonArray)
            {
                var crypto = new CryptoCurrency
                {
                    Id = item["id"].ToString(),
                    Symbol = item["symbol"].ToString(),
                    Name = item["name"].ToString(),
                    CurrentPrice = (decimal)item["current_price"],
                    MarketCap = (long)item["market_cap"],
                    TotalVolume = (long)item["total_volume"],
                    PriceChangePercentage24h = (decimal)item["price_change_percentage_24h"]
                };
                cryptoList.Add(crypto);
            }

            return cryptoList;
        }

        public async Task<JObject> GetHistoricalDataAsync(string id, string currency = "usd", int days = 30)
        {
            var response = await _httpClient.GetStringAsync($"coins/{id}/market_chart?vs_currency={currency}&days={days}");
            return JObject.Parse(response);
        }
    }
}