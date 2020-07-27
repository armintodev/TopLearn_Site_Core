using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if(!ModelState.IsValid)
            {
                return View(register);
            }
            if(_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName","نام کاربری معتبر نمی باشد");
                return View(register);
            }
            if(_userService.IsExistEmail(FixedText.FixedEmail(register.Email)))
            {
                ModelState.AddModelError("Email","ایمیل معتبر نمی باشد");
                return View(register);
            }

            User user = new User()
            {
                ActiveCode = NameGenerator.GenerateUniqCode(),
                Email = FixedText.FixedEmail(register.Email),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "Default.jpg",
                UserName = register.UserName

            };
            _userService.AddUser(user);

            //TODO: Send Activation Email ...!

            return View("SuccessRegister",user);
        }
        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {

            if(!ModelState.IsValid)
            {
                return View(login);
            }
            var user = _userService.LoginUser(login);
            if(user != null)
            {
                if(user.IsActive)
                {
                    //TODO: Login User ...!
                    ViewBag.IsSuccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email","حساب کاربری شما فعال نمی باشد");
                }
            }
            ModelState.AddModelError("Email","کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }

        #endregion

        #region Active Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);

            return View();
        }

        #endregion
    }
}
