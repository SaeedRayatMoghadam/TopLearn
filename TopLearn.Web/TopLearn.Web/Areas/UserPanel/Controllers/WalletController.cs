using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs.UserPanel;
using TopLearn.Core.Interfaces;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            ViewBag.TransactionList = _walletService.GetUserTransactions(User.Identity.Name);
            return View();
        }


        [Route("UserPanel/Wallet")]
        [HttpPost]
        public IActionResult Index(ChargeWalletViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TransactionList = _walletService.GetUserTransactions(User.Identity.Name);
                return View(model);
            }

            //TODO: Payment Gateway
            
            _walletService.ChargeWallet(User.Identity.Name, model.Amount);

            return null;    
        }
    }
}
