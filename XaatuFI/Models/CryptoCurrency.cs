﻿namespace CryptoMarketTracker.Models
{
    public class CryptoCurrency
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal MarketCap { get; set; }
        public decimal TotalVolume { get; set; }
        public decimal PriceChangePercentage24h { get; set; }
    }
}