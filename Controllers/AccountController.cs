using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tedarix.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using Tedarix.Models.Entities;

namespace Tedarix.Controllers
{
    public class AccountController : Controller
    {
        private readonly string privateKey = "";
        private readonly string oldkey = "";
        public string HtmlScriptRegex(string htmlText)
        {
            string textReplace = htmlText;
            string regexPattern = @"[<>£#$½\[\]\}\|\{]|javascript|script";
            if (Regex.IsMatch(textReplace, regexPattern, RegexOptions.IgnoreCase))
            {
                Regex specialCharsRegex = new Regex(regexPattern, RegexOptions.IgnoreCase);
                htmlText = specialCharsRegex.Replace(textReplace, "");

            }
            return htmlText;
        }
        public string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = $"{password}{salt}";
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashedBytes);
            }
        }
        public async Task<IActionResult> Login(string Name, string Password)
        {

            if (User.Identity.IsAuthenticated) // Check if the user is already authenticated
            {
                return RedirectToAction("Index", "Home");
            }

            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Password))
            {
                Password = HtmlScriptRegex(Password);
                string hashedPassword = HashPassword(Password, privateKey + oldkey);

                using (TedarixContext db = new TedarixContext())
                {
                    var kullanici = await db.Users.FirstOrDefaultAsync(a => a.Name == Name && a.Password == hashedPassword);

                    if (kullanici != null)
                    {
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Kullanici"),
                    new Claim(ClaimTypes.Name, kullanici.Name),
                    new Claim("Password", hashedPassword),
                    new Claim("Id", kullanici.Id.ToString())
                };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme
                        );

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties
                        );

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View(); // Uyarı göster
                    }
                }
            }
            return View();
        }

    }
}
