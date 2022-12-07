using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs.Account;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Security;
using TopLearn.Core.Utilities;
using TopLearn.Data.Entities.User;
using TopLearn.Web.ViewModels.Account;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost("Register")]
        public IActionResult Register(RegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (_accountService.IsUserNameExist(user.UserName))
            {
                ModelState.AddModelError("UserName","نام کاربری دردرسترس نمیباشد");
                return View(user);
            }

            if (_accountService.IsEmailExist(EmailFixer.Fix(user.Email)))
            {
                ModelState.AddModelError("Email","ایمیل وارد شده دردسترس نمیباشد");
                return View(user);
            }

            var createdUser = new User()
            {
                ActiveCode = CodeGenerator.Generate(),
                UserName = user.UserName,
                Email = user.Email,
                Password = PasswordHelper.EncodeToMd5(user.Password),
                Avatar = "Default.jpg",
                IsActive = false,
                RegisterDate = DateTime.Now
            };

            _accountService.Register(createdUser);

            return View("RegisteredSuccessfuly",createdUser);
        }


        #endregion


        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var login = _accountService.Login(user);
            if (login != null)
            {
                if (login.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, login.Id.ToString()),
                        new Claim(ClaimTypes.Name, login.UserName),
                        new Claim(ClaimTypes.Email, login.Email)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principals = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties()
                    {
                         IsPersistent = user.RememberMe
                    };
                    HttpContext.SignInAsync(principals, properties);


                    ViewBag.IsActive = true;
                    return Redirect("/");
                }
                ModelState.AddModelError("Email","حساب کاربری شما فعال نمیباشد.");
                return View(user);
            }

            ModelState.AddModelError("Email", "حسابی با مشخصات وارد شده یافت نشد.");
            return View(user);
        }

        #endregion


        #region Logout

        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion


        #region ActiveAccount

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _accountService.ActiveAccount(id);
            return View();
        }

        #endregion

    }
}
