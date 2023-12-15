using Microsoft.AspNetCore.Mvc;

namespace InfinitySystems.Controllers
{
    public class MainPageController : Controller
    {
        public IActionResult MainPage()
        {
            return View();
        }

        public IActionResult Login_Register()
        {
            return RedirectToAction("Login_Register", "Login_Register");
        }

    }
}
