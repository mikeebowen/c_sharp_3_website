using MiniCStructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
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
            User newUser = new User();
            return View(newUser);
        }
        [HttpPost]
        public async Task<ActionResult> Index(string email, string password, string passwordConfirm)
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
            catch(DbUpdateException ex)
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
    }
}
