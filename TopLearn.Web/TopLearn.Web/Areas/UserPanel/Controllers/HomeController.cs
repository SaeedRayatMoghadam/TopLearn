using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs.UserPanel;
using TopLearn.Core.Interfaces;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetUserInfo(User.Identity.Name));
        }


        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            return View(_userService.GetUserForEditProfile(User.Identity.Name));
        }

        [HttpPost("UserPanel/EditProfile")]
        public IActionResult EditProfile(EditProfileViewModel model)
        {  
            if (!ModelState.IsValid)
                return View(model);

            _userService.EditProfile(User.Identity.Name, model);
            return Redirect("/Logout");
        }


        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost("UserPanel/ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if(!ModelState.IsValid)
                return View();

            if (_userService.ComparePassword(User.Identity.Name, model.CurrentPassword))
            {
                _userService.ChangePassword(User.Identity.Name,model.Password);
                return Redirect("/Logout");
            }
            else
            {
                ModelState.AddModelError("ConfirmPassword","اطلاعات وارد شده همخوانی ندارند");
                return View(model);
            }
        }
    }
}
