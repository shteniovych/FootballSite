using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FC_B13.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FC_B13.Controllers
{
    public class HomeController : Controller
    {
        //[Authorize(Roles = "admin, user")]
        public IActionResult Index()
        {
            //if (User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value != null)
            //{
            //    string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            //    //return Content($"ваша роль: {role}");
            //    ViewData["Message"] = role;
            //}
            //string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            //return Content($"ваша роль: {role}");
            //ViewData["Message"] = role;

            return View();
        }
        //[Authorize(Roles = "admin")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
    }
}
