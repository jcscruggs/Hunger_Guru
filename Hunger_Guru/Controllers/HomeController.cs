using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hunger_Guru.Models;
using System.Net.Http;

namespace Hunger_Guru.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _clientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            // read key from ZomatoAPIkey.txt. this is done to keep api key out of source control(GitHub)
            String key = System.IO.File.ReadAllText("ZomatoAPIkey.txt");

            // create request (query) to attach to base address
            var request = new HttpRequestMessage(HttpMethod.Get, "api/v2.1/cities?q=Greenville%2Csc");

            // retrieve base address
            var client = _clientFactory.CreateClient("zomato");

            // attach API key header
            client.DefaultRequestHeaders.Add("user-key", key);

            // wait for response from clent request
            var response = await client.SendAsync(request);

           
            String responseString = await response.Content.ReadAsStringAsync();

            ViewData["data"] = responseString;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
