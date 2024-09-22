using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR_Managament_System.Models;


namespace HR_Managament_System.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/SignIn
        [HttpGet]
        public ActionResult SignIn()
        {
            ViewBag.Title = "Sign In Page";
            return View(); // Ensure this renders the SignIn view
        }

        [HttpPost]
        public ActionResult SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                // Validate the user
                bool isValidUser = SignInModel.checkValidateUser(model.UserName, model.Password);

                if (isValidUser)
                {
                    // Redirect to EmployeeList action in EmployeeController
                    return RedirectToAction("Index", "MainHome");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid credentials. Please check your username and password.");
                }
            }
            // Return the view with the current model (including validation errors)
            return View(model);
        }
    }
}
