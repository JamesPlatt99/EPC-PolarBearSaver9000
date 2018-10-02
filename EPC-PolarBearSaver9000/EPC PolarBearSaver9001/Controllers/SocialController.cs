﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DBContext.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Data.Entity;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EPC_PolarBearSaver9001.Controllers
{
    public class SocialController : Controller
    {
        #region Properties

        private readonly DBContext.Models.EPC_PolarBearSaver9001Context _context = DBContext.ContextBuilder.GetContext();
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
        #endregion

        #region Constructor     

        public SocialController(IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Actions
        public IActionResult Index()
        {
            Models.SocialModel model = CreateNewModel();
            var test = LoggedInUser;
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string searchQuery)
        {
            Models.SocialModel model = CreateNewModel();
            model.SearchResults = GetSearchResults(searchQuery, model);
            return View(model);
        }

        [HttpPost]
        [ActionName("AddFriend")]
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
            Models.SocialModel model = CreateNewModel();
            return View("Index", model);
        }

        [HttpPost]
        [ActionName("RemoveFriend")]
        public IActionResult RemoveFriend(string userID)
        {
            Friends friendToRemove = _context.Friends.Where(n =>n.User1Id == LoggedInUser.Id && n.User2Id == userID).First();
            _context.Remove(friendToRemove);
            _context.SaveChanges();
            Models.SocialModel model = CreateNewModel();
            return View("Index", model);
        }
        #endregion

        #region Private Methods

        private List<AspNetUsers> GetSearchResults(string searchQuery, Models.SocialModel model)
        {
            List<AspNetUsers> usersToExclude = new List<AspNetUsers>();
            usersToExclude.AddRange(model.Friends);
            usersToExclude.Add(LoggedInUser);

            return _context.AspNetUsers.Where(n => n.UserName.Contains(searchQuery))
                                .Except(usersToExclude).ToList();
        }

        private Models.SocialModel CreateNewModel()
        {


            Models.SocialModel model = new Models.SocialModel
            {
                Friends = GetFriends(),
                SearchResults = new List<AspNetUsers>()
            };
            var test = _context.Friends.Where(n => n.User1Id == LoggedInUser.Id)
                                          .Select(n => n.User2.Address).Where(n=>n != null).ToList();
            return model;
        }

        private List<AspNetUsers> GetFriends()
        {
            var friends = _context.Friends.Where(n => n.User1Id == LoggedInUser.Id)
                                          .Select(n => n.User2);
            PrefetchAddresses(friends);
            return friends.ToList();
        }

        private void PrefetchAddresses(IQueryable<AspNetUsers> users)
        {
            IEnumerable<Address> curAddress;
            foreach(AspNetUsers user in users)
            {
                curAddress = user.Address;
            }
        }
        #endregion

    }
}
