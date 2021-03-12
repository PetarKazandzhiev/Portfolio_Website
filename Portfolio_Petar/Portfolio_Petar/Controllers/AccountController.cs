using Portfolio_Petar.Models;
using Portfolio_Petar.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace Portfolio_Petar.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private Portfolio_DBEntities _database;

        public AccountController()
        {
            _database = new Portfolio_DBEntities();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model){

            var FoundUser = _database.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (FoundUser == null )
            {

                TempData["Error"] = "The Username or Password do not match a record in the Database ";
                return View(model);
            }
            //if someone i already logged in but wants to log in from a different acc this will 
            //reset the session and log him out
            Session.Clear();
            Session["Loggedin"] = true;
            Session["Username"] = FoundUser.Username;
            Session["ID"] = FoundUser.ID;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register(){
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            
            if ( string.IsNullOrWhiteSpace(model.Username) || model.Username.Length > 20){
                TempData["Error"] = "The Username is empty or above 20 caracters";
                return View(model);
            }
            if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length > 20|| model.Password.Length < 4)
            {
                TempData["Error"] = "The Password is empty, above 20 caracters or below 4 caracters";
                return View(model);
            }
            if (model.ConfirmPassword != model.Password)
            {
                TempData["Error"] = "The Password does not match the Retype Password ";
                return View(model);
            }
            if (string.IsNullOrWhiteSpace(model.Email) || model.Email.Length > 30)
            {
                TempData["Error"] = "The Email is empty or above 30 caracters";
                return View(model);
            }
            //Checks if there is a user with the same name in tne database
            if (_database.Users.Any(u=>u.Username==model.Username)){

                TempData["Error"] = "The Username is already taken";
                return View(model);

            }
            _database.Users.Add(new User()
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email
            }) ;
            _database.SaveChanges();

            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}