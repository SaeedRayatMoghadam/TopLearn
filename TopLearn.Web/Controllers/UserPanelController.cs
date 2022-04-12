using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Utils;
using TopLearn.Core.ViewModels.UserPanel;

namespace TopLearn.Web.Controllers
{
    public class UserPanelController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWalletService _walletService;

        public UserPanelController(IUserService userService, IWalletService walletService)
        {
            _userService = userService;
            _walletService = walletService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetUserInfo(User.Identity.Name));
        }

        
        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            return View(_userService.GetUserForEdit(User.Identity.Name));
        }

        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditUserProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _userService.EditProfile(User.Identity.Name, model);

            return Redirect("/Logout");
        }


        [Route("/UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("/UserPanel/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userName = User.Identity.Name;
            if (!_userService.CheckPassword(userName, model.CurrentPassword))
            {
                ModelState.AddModelError("CurrentPassword", "Password is WRONG !");
                return View(model);
            }


            _userService.ChangePassword(userName, model.NewPassword);

            return Redirect("/Logout");
        }

        [Route("/UserPanel/Wallet")]
        [HttpGet]
        public IActionResult Wallet()
        {
            ViewBag.TransactionHistory = _walletService
                .GetWalletHistory(_userService.GetUserId(User.Identity.Name));
            return View();
        }
        
        [Route("UserPanel/ChargeWallet")]
        [HttpPost]
        public IActionResult ChargeWallet(ChargeWalletViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TransactionHistory = _walletService
                    .GetWalletHistory(_userService.GetUserId(User.Identity.Name));
                return RedirectToAction("Wallet",model);
            }

            _walletService
                .ChargeWallet(_userService.GetUserId(User.Identity.Name),model.Amount,"Deposit to Account");

            //TODO Online Payment

            return null;
        }
    }
}
