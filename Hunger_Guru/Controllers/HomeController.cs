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
        private readonly String key;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _clientFactory = httpClientFactory;
            // read key from ZomatoAPIkey.txt. this is done to keep api key out of source control(GitHub)
             key = System.IO.File.ReadAllText("ZomatoAPIkey.txt");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult City()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> City(CityInfo cityInfo)
        {
            ViewData["error"] = "";
            // check if the data model required input is satsified
            if (!ModelState.IsValid)
            {
                return View(cityInfo);
            }
            
            // create request (query) to attach to base address
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/v2.1/cities?q=" + cityInfo.City + "%2C" + cityInfo.State);

            String responseString = await APIbuilderAsync(request);

            Location location = Locations.FromJson(responseString);

            // if the request response is empty or the most likely city does not match users input
            if(location.LocationSuggestions.Count < 1 || !location.LocationSuggestions[0].Name.ToLower().Equals( (cityInfo.City + ", " + cityInfo.State).ToLower()))
            {
                // return to the city page and notify the user of error
                ViewData["error"] = "The Guru was unable to find a city named " + cityInfo.City;
                return View();
            }

            TempData["data"] = responseString;

            return RedirectToAction(nameof(State));
        }

        public IActionResult State()
        {
            // test for if the request response is recieved
            ViewData["data"] = TempData["data"];
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<string> APIbuilderAsync(HttpRequestMessage httpRequestMessage)
        {
            // retrieve base address
            var client = _clientFactory.CreateClient("zomato");

            // attach API key header
            client.DefaultRequestHeaders.Add("user-key", key);

            // wait for response from clent request
            var response = await client.SendAsync(httpRequestMessage);


            String responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}
