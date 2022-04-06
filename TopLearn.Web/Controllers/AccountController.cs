using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Senders;
using TopLearn.Core.Utils;
using TopLearn.Core.ViewModels.Account;
using TopLearn.Data.Models.Users;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _renderService;

        public AccountController(IUserService userService, IViewRenderService renderService)
        {
            _userService = userService;
            _renderService = renderService;
        }

        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userService.Login(model);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Email,user.Email)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principals = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = model.RememberMe
                    };

                    HttpContext.SignInAsync(principals, properties);

                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("Email", "Your Account Is NOT Active !");
                    return View(model);
                }
            }

            ModelState.AddModelError("Email", "Invalid Inputs .");
            return View(model);
        }

        #endregion

        #region Register

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_userService.IsUserNameExist(model.UserName))
            {
                ModelState.AddModelError("UserName", "UserName NOT Valid !");
                return View(model);
            }

            if (_userService.IsEmailExist(model.Email.FixEmail()))
            {
                ModelState.AddModelError("Email", "Email NOT Valid !");
                return View(model);
            }


            var user = new User()
            {
                UserName = model.UserName,
                ActiveCode = Generator.GenerateGuid(),
                Email = model.Email.FixEmail(),
                IsActive = false,
                Password = model.Password.EncodeToMd5(),
                RegisterDate = DateTime.Now,
                Avatar = "avatar.jpg"
            };

            _userService.Register(user);

            //Send Activation Email
            //string body = _renderService.RenderToStringAsync("_ActiveEmail", user);
            //EmailSender.Send(user.Email, "Account Activation", body);

            return RedirectToAction("Login");
        }

        #endregion

        #region LogOut

        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion

        #region Activate Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActivateAccount(id);

            return View();
        }

        #endregion

        #region ForgotPassword

        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userService.Get(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email","Invalid Inputs .");
                return View(model);
            }

            string body = _renderService.RenderToStringAsync("_ForgotPassword", user);
            EmailSender.Send(user.Email, "Reset Password", body);

            return View();
        }

        #endregion

        #region Reset Password

        [Route("ResetPassword")]
        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            });
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userService.GetByActiveCode(model.ActiveCode);

            if (user == null)
                return NotFound();

            user.Password = model.Password.EncodeToMd5();
            _userService.Update(user);

            return Redirect("/Login");
        }

        #endregion
    }
}
