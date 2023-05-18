using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Todo.Models;

namespace Todo.Controllers
{
    public class LoginTodoController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Read", "Item");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginItemTodo loginModel)
        {
            if(loginModel.Username == "user" && loginModel.Password == "pwd"){
                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, loginModel.Username)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, 
                    CookieAuthenticationDefaults.AuthenticationScheme
                    );
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = loginModel.IsLoggedIn
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Read", "Item");
            }
            ViewData["ValidateMessage"]="User Not Found";
            return View();
        }

    }
}
