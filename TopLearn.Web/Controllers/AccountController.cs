using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Utils;
using TopLearn.Core.ViewModels.Account;
using TopLearn.Data.Models.Users;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
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
                    //TODO : Login
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

            //TODO: Send Activation Email

            return RedirectToAction("Login");
        }

        #endregion


        #region Activate Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActivateAccount(id);

            return View();
        }

        #endregion
    }
}
