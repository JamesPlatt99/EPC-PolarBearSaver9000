using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DBContext.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EPC_PolarBearSaver9001.Controllers
{
    public class SocialController : Controller
    {
        private readonly DBContext.Models.EPC_PolarBearSaver9001Context _context = DBContext.Context.polarBearSaver9001Context;
        private IHttpContextAccessor HttpContextAccessor { get; set; }

        private AspNetUsers _loggedInUser;
        private AspNetUsers LoggedInUser
        {
            get
            {
                if(_loggedInUser == null && HttpContextAccessor.HttpContext.User != null)
                {
                    var userId = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    _loggedInUser = _context.AspNetUsers.Where(n => n.Id == userId).Single();
                }
                return _loggedInUser;
            }
        }

        public SocialController(IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            Models.SocialModel model = new Models.SocialModel();
            model.Friends = new List<DBContext.Models.AspNetUsers>();
            model.SearchResults = new List<DBContext.Models.AspNetUsers>();
            var test = LoggedInUser;
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string searchQuery)
        {
            Models.SocialModel model = new Models.SocialModel();
            model.Friends = new List<AspNetUsers>();
            model.SearchResults = _context.AspNetUsers.Where(n => n.UserName.Contains(searchQuery)).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddFriend(string userID)
        {
            AspNetUsers friendToAdd = _context.AspNetUsers.Where(n => n.Id == userID).Single();
            var newFriends = new Friends()
            {
                User1 = LoggedInUser,
                User2 = friendToAdd
            };
            _context.Add(newFriends);
            _context.SaveChanges();
            return Index();
        }

    }
}
