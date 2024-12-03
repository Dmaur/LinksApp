using Microsoft.AspNetCore.Mvc;
using LinksApp.Models;
using Microsoft.AspNetCore.Http;
using DotNetEnv;

namespace LinksApp.Controllers
{
    public class LoginController : Controller
    {
        private string connectionString;

        public LoginController(){
            Env.Load();
            connectionString = Env.GetString("CONNECTION_STRING");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Submit(string MyUserName, string MyPassword)
        {
            // construct manually to pass in connection string and http context
            LogIn login = new LogIn(connectionString, HttpContext);
            // update properties
            login.Username = MyUserName;
            login.password = MyPassword;

            // attempt unlock
            if (login.Unlock()){
                
                return RedirectToAction("Index", "Admin");
            }else{
                // incorrect login - update feedback
                
                ViewData["feedback"] = "Incorrect login. please try again";
            }
        
            return View("Index");
        }

        public IActionResult  logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}