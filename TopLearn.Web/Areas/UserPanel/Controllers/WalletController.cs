using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    public class WalletController : Controller
    {
        #region Injection

        private IUserService _userService;
        public WalletController(IUserService userService) => _userService = userService;
        #endregion
        [Area("UserPanel")]
        [Authorize]
        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
            return View();
        }
        [Route("UserPanel/Wallet")]
        [HttpPost]
        public IActionResult Index(ChargeWalletViewModel charge)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
                return View(charge);
            }

           int walletId= _userService.ChargeWallet(User.Identity.Name,charge.Amount,"شارژ حساب");

            #region Online Payment


            var payment = new ZarinpalSandbox.Payment(charge.Amount);
            var res = payment.PaymentRequest("شارژ کیف پول", "http://localhost:44398/OnlinePayment/" + walletId, "arminprgrmcsfamily@gmail.com", "09113208254");

            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }


            #endregion


            return null;
        }
    }


}
