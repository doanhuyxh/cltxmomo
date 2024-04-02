using cltxmomo.Models.UserVM;
using Microsoft.AspNetCore.Mvc;

namespace cltxmomo.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet("dang-nhap")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM vm)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
