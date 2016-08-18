using SeaWarServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaWarServer.Controllers
{
    public class HomeController : Controller
    {
        static DataBaseContext dbContext = new DataBaseContext();
        // GET: Home
        public ActionResult Index()
        {
            return View(dbContext.Users.ToList());
        }

        public ActionResult Api()
        {
            return View();
        }
    }
}