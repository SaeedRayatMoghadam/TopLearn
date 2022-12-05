using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Security;
using TopLearn.Core.Utilities;
using TopLearn.Data.Entities.User;
using TopLearn.Web.ViewModels.Account;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

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
    }
}
