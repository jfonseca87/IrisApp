using IrisApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace IrisApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProcessFileService _processFileService;

        public HomeController(ILogger<HomeController> logger, IProcessFileService processFileService)
        {
            _logger = logger;
            _processFileService = processFileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<string> ProcessFiles()
        {
            var result = await _processFileService.ProcessClientInformation();
            return JsonConvert.SerializeObject(result);
        }
    }
}