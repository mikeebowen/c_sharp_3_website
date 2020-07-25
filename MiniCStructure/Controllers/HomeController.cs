using MiniCStructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MiniCStructure.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(string password, string email)
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
        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            //User user = await MiniCStructure.Models.User.GetByEmail("admin");
            //return Content(user.UserEmail);
            User newUser = new User();
            return View(newUser);
        }
        [HttpPost]
        public async Task<ActionResult> Register(string email, string password, string passwordConfirm)
        {
            User newUser = new MiniCStructure.Models.User
            {
                UserEmail = email,
                UserPassword = password
            };
            ValidationContext validationContext = new ValidationContext(newUser);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(newUser, validationContext, results, true);
            List<string> errors = new List<string>();
            foreach (ValidationResult result in results)
            {
                errors.Add(result.ToString());
            }
            TempData["errorMessages"] = errors;
            if (errors.Count > 0)
            {
                return View(newUser);
            }
            try
            {
                int id = await MiniCStructure.Models.User.Create(newUser);
                newUser.UserId = id;
                Session["user"] = newUser;
                return Redirect("/");
            }
            catch (DbUpdateException ex)
            {
                errors.Add("That email has already been registered");
                TempData["errorMessages"] = errors;
                return View(newUser);
            }
            catch (Exception ex)
            {
                errors.Add("There was a problem creating your account");
                TempData["errorMessages"] = errors;
                return View(newUser);
            }
        }
        [HttpGet]
        public async Task<ActionResult> ClassList()
        {
            List<Class> classes = await Class.GetAll();
            return View(classes);
        }
        [HttpGet]
        public async Task<ActionResult> EnrollInClass()
        {
            List<Class> classes = await Class.GetAll();
            return View(classes);
        }
        [HttpPost]
        public ActionResult EnrollInClass(string foo)
        {
            return Redirect("/");
        }
    }
}