using Bloomfiy.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;


namespace Bloomfiy.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model, string ConfirmPassword)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please fill the form correctly.";
                return View(model);
            }

            if (model.Password != ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View(model);
            }

            if (db.Users.Any(u => u.Username == model.Username))
            {
                ViewBag.Error = "Username already exists.";
                return View(model);
            }

            model.Password = PasswordHelper.HashPassword(model.Password);
            model.Role = "User";

            db.Users.Add(model);
            db.SaveChanges();

            // Success message via TempData
            TempData["RegisterSuccess"] = "Registration successful! You are now logged in.";

            // Auto-login after register
            FormsAuthentication.SetAuthCookie(model.Username, false);
            Session["Role"] = model.Role;

            return RedirectToAction("Index", "Home");
        }



        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Please provide username and password.";
                return View();
            }

            string hashed = PasswordHelper.HashPassword(password);
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == hashed);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            // Set auth cookie and role session
            FormsAuthentication.SetAuthCookie(user.Username, false);
            Session["Role"] = user.Role;

            // Redirect to home (or profile)
            return RedirectToAction("Index", "Home");
        }


        // GET: /Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Profile
        [Authorize]
        public ActionResult Profile()
        {
            string username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            return View(user);
        }
    }
}
