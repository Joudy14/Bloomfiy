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
        public ActionResult Register(User model, string passwordConfirm)
        {
            if (model.Password != passwordConfirm)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            // Check if username already exists
            if (db.Users.Any(u => u.Username == model.Username))
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            // Hash password
            string hashed = PasswordHelper.HashPassword(model.Password);
            model.Password = hashed;
            model.Role = "User"; // every new register = normal user

            db.Users.Add(model);
            db.SaveChanges();

            FormsAuthentication.SetAuthCookie(model.Username, false);

            return RedirectToAction("Profile");
        }


        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // hashing password
            string hashedPassword = PasswordHelper.HashPassword(password);

            var user = db.Users.FirstOrDefault(u =>
                u.Username == username && u.Password == hashedPassword);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("Profile", "Account");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
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
