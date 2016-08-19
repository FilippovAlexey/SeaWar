using SeaWarServer.Models;
using SeaWarServer.NonProject;
using System.Linq;
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
            result.Reverse();
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(string Message, string Author)
        {
            dbContext.Book.Add(new BookItem() { Autor = Author, Message = Message });
            dbContext.SaveChanges();
            var result = dbContext.Book.Reverse().ToList();
            return View(result);
        }
    }
}