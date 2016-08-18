﻿using SeaWarServer.Models;
using SeaWarServer.NonProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaWarServer.Controllers
{
    public class BookController : Controller
    {
        DataBaseContext dbContext = new DataBaseContext();
        [HttpGet]
        public ActionResult Index()
        {
            var result = dbContext.Book.ToList();
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(string Message, string Author)
        {
            dbContext.Book.Add(new BookItem() { Autor = Author, Message = Message });
            dbContext.SaveChanges();
            var result = dbContext.Book.ToList();
            return View(result);
        }
    }
}