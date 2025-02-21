using Microsoft.AspNetCore.Mvc;
using CryptoMarketTracker.Services;
using System.Threading.Tasks;

namespace CryptoMarketTracker.Controllers
{

    
    public class NewsController : Controller
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

    

        public async Task<IActionResult> Index()
{
    var news = await _newsService.GetCryptoNewsAsync();
    return View("~/Views/Crypto/News.cshtml", news); 
}
    }
}