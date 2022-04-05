using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eShop.UI.Server.Controllers;
public class UserController : Controller
{
    private const string coockieName = "eShop.CookieAuth";

    [Route("login")]
    public async Task<IActionResult> Login([FromQuery] string user, [FromQuery] string pwd)
    {
        if (user == "admin" && pwd == "password")
        {
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.Email, "admin@shop.com"),
                new Claim(ClaimTypes.HomePhone, "1212121212")
            };

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, coockieName));
            await HttpContext.SignInAsync(coockieName, userPrincipal);
        }
        return Redirect("/outstandingOrders");
    }

    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/outstandingOrders");
    }
}
