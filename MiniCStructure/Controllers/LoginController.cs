using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MiniCStructure.Models;

namespace MiniCStructure.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(string password, string email)
        {
            User user = await MiniCStructure.Models.User.CheckPassword(password, email);
            if (user == null)
            {
                TempData["errorMessage"] = "email or password is incorrect";
                return View();
            }
            else
            {
                Session["user"] = user;
                return Redirect("/");
            }
        }
    }
}