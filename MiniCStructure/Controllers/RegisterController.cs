using MiniCStructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MiniCStructure.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        [HttpGet]
        public ActionResult Index()
        {
            //User user = await MiniCStructure.Models.User.GetByEmail("admin");
            //return Content(user.UserEmail);
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(string email, string password, string passwordConfirm)
        {
            User newUser = new MiniCStructure.Models.User
            {
                UserEmail = email,
                UserPassword = password
            };
            await MiniCStructure.Models.User.Create(newUser);
            return Content($"{email}, {password}, {passwordConfirm}");
        }
    }
}
