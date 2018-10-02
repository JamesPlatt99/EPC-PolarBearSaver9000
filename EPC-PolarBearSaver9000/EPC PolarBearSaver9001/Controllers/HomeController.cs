using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EPC_PolarBearSaver9001.Models;
using EPCPolarBearSaver9001.Models;

namespace EPC_PolarBearSaver9001.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RewardsPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult API()
        {
            
              AddressFinder addressFinder = new AddressFinder
              {
                  Bubbles = Enumerable.Empty<string>()
              };

            return View(addressFinder);
        }

        [HttpPost]
        public IActionResult API(string Postcode)
        {
            AddressFinder addressFinder = new AddressFinder();
            IEnumerable<string> list = EPCAPICaller.APIRequest.GetAddresses(Postcode);
            //if (addressFinder.Bubbles == null)
            //{
            //    List<string> list2 = new List<string>();
            //    foreach (var item in list)
            //    {
            //        list2.ToList().Add(item);
            //    }

            //}
            //else
            //{
            //    addressFinder.Bubbles.ToList().Add("These are the addresses");
            //    foreach (var item in list)
            //    {
            //        addressFinder.Bubbles.ToList().Add(item);
            //    }
            //}
            AddressFinder addressFinder2 = new AddressFinder
            {
                 Bubbles=list,
            };

            return this.View(addressFinder2);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
