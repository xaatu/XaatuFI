using Microsoft.AspNetCore.Mvc;
using CryptoMarketTracker.Services;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CryptoMarketTracker.Controllers
{
    public class CryptoController : Controller
    {
        private readonly CryptoService _cryptoService;
        private readonly NewsService _newsService;

        public CryptoController(CryptoService cryptoService, NewsService newsService)
        {
            _cryptoService = cryptoService;
            _newsService = newsService;
        }

        public async Task<IActionResult> Index(string currency = "usd")
        {
            var cryptos = await _cryptoService.GetTopCryptosAsync(currency: currency);
            ViewBag.Currency = currency.ToUpper();
            return View(cryptos);
        }

        public async Task<IActionResult> News()
        {
            var news = await _newsService.GetCryptoNewsAsync();
            return View(news); 
        }

        public async Task<IActionResult> HistoricalData(string id, string currency = "usd", int days = 30)
        {
            var historicalData = await _cryptoService.GetHistoricalDataAsync(id, currency, days);
            ViewBag.CryptoId = id;
            ViewBag.Currency = currency;
            ViewBag.Days = days;
            return View(historicalData);
        }
    }
}