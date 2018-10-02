using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EPC_PolarBearSaver9001.Models;
using EPCPolarBearSaver9001.Models;
using Microsoft.AspNetCore.Http;
using DBContext.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace EPC_PolarBearSaver9001.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
        }
        #region Properties

        private readonly DBContext.Models.EPC_PolarBearSaver9001Context _context = DBContext.ContextBuilder.GetContext();
        private IHttpContextAccessor HttpContextAccessor { get; set; }

        private AspNetUsers _loggedInUser;
        private AspNetUsers LoggedInUser
        {
            get
            {
                if (_loggedInUser == null && HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null)
                {
                    var userId = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    _loggedInUser = _context.AspNetUsers.Where(n => n.Id == userId).Include(n=>n.Address).Single();
                }
                return _loggedInUser;
            }
        }
        #endregion

        public IActionResult RewardsPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            Address address = null;                
            if (LoggedInUser != null){address = _context.Address.Where(n => n.UserId == LoggedInUser.Id).SingleOrDefault();}

            AddressFinder addressFinder = new AddressFinder
            {
                Address1 = address?.AddressLine1 ?? String.Empty,
                Postcode = address?.PostCode ?? String.Empty,
            };

            return View(addressFinder);
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
