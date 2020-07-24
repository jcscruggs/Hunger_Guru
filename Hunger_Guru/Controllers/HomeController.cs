﻿using System;
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
        private readonly IHttpClientFactory _clientFactory1;
        private readonly String key;
        private readonly SearchCriteria searchCriteria;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _clientFactory = httpClientFactory;
            _clientFactory1 = httpClientFactory;
            // read key from ZomatoAPIkey.txt. this is done to keep api key out of source control(GitHub)
             key = System.IO.File.ReadAllText("ZomatoAPIkey.txt");
            this.searchCriteria = new SearchCriteria();
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


            

            TempData["city_id"] = location.LocationSuggestions[0].Id;

            return RedirectToAction("CuisineChoice");
        }

        public async Task<IActionResult> CuisineChoice()
        {
            int city_id = (int)TempData["city_id"];
            // build request string
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/v2.1/cuisines?city_id=" + city_id);

            String responseString = await APIbuilderAsync(request);

            //ViewData["data1"] = city_id;
            

            Cuisine cuisine = Cuisine.FromJson(responseString);

            // add form to view that holds checkboxes for cuisine types. 

            List<CuisineViewModel> cuisines = new List<CuisineViewModel>();

            for(int i = 0; i < cuisine.Cuisines.Count; i++)
           {
                

              String name = cuisine.Cuisines[i].Cuisine.CuisineName;

                cuisines.Add(new CuisineViewModel
                {
                    city_id = city_id,
  
                  ID = cuisine.Cuisines[i].Cuisine.CuisineId,

                    name = cuisine.Cuisines[i].Cuisine.CuisineName,

                    
                }
                ) ;
            }

            ViewData["error"] = "";

            return View(cuisines);
        }

        [HttpPost]
        public IActionResult CuisineChoice(List<CuisineViewModel> cuisinelist)
        {
            

            int city_id = cuisinelist[0].city_id;

            String cuisineChoices = "";

            for (int i = 0; i < cuisinelist.Count; i++)
            {
                if (cuisinelist[i].Cuisine_Wanted)
                {
                    cuisineChoices += (cuisinelist[i].ID + "%2C");
                }
            }
            
            if(cuisineChoices.Length == 0)
            {
                ViewData["error"] = "The Guru needs you to choose atleast one cuisine";
                
                return View(cuisinelist);
            }

            cuisineChoices = cuisineChoices.TrimEnd('%','2','C');

            TempData["cuisinechoices"] = cuisineChoices;
            TempData["cityid"] = city_id;
            

            return RedirectToAction("restaurants");
        }

        public async Task<IActionResult> restaurants()
        {
            int city_id = (int)TempData["cityid"];
            string cuisineChoices = (String)TempData["cuisinechoices"];//(String)TempData["cuisinechoices"];

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/v2.1/search?entity_id=" + city_id + "&entity_type=city&count=5&cuisines=" + cuisineChoices);

            var client = _clientFactory1.CreateClient("zomato");

            // attach API key header
            client.DefaultRequestHeaders.Add("user-key", key);

            // wait for response from clent request
            var response = await client.SendAsync(request);

            String responseString = await response.Content.ReadAsStringAsync(); ;

            Search searchresponse = Search.FromJson(responseString);

            ViewData["data2"] = responseString;

            return View(searchresponse);

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
