using Portfolio_Petar.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio_Petar.Controllers
{
    public class HomeController : Controller
    {

        private Portfolio_DBEntities _database;

        public HomeController()
        {
            _database = new Portfolio_DBEntities();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gallery()
        {
      
            return View();
        }
        [HttpPost]
        public ActionResult PostComment(string comment, string returnUrl)
        {
            var Loggedin = ((Session?["Loggedin"] as bool?) ?? false);

            if (string.IsNullOrWhiteSpace(comment) || comment.Length > 140) {
                TempData["Error"] = "the comment is empty or above 140 caracters";
            }
            else{
                _database.Comments.Add(new Comment()
                {
                    CommentText = comment,
                    Date = DateTime.UtcNow,
                    UserID = Loggedin ? Session["ID"] as int? : null

                }) ;
                _database.SaveChanges();
            }
            return Redirect(returnUrl+ "#comments");

        }

    }

    
}