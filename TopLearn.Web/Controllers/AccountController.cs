using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Senders;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _viewRender;
        public AccountController(IUserService userService,IViewRenderService viewRender)
        {
            _userService = userService;
            _viewRender = viewRender;
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

            #region Send Activation Email

            string body = _viewRender.RenderToStringAsync("_ActiveEmail",user);
            SendEmail.Sendemail(user.Email,"فعال سازی",body);
            #endregion

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
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal,properties);

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

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion

        #region Active Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);

            return View();
        }

        #endregion

        #region Forgot Password

        [Route("ForgotPassword")]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if(!ModelState.IsValid)
            {
                return View(forgotPassword);
            }
            var fixEmail = FixedText.FixedEmail(forgotPassword.Email);
            User user = _userService.GetUserByEmail(fixEmail);
            if(user == null)
            {
                ModelState.AddModelError("Email","کاربری یافت نشد");
                return View(forgotPassword);
            }
            string bodyEmail = _viewRender.RenderToStringAsync("_ForgotPassword",user);
            SendEmail.Sendemail(user.Email,"بازیابی کلمه عبور",bodyEmail);
            ViewBag.IsSuccess = true;
            return View();
        }

        #endregion

        #region Reset Password

        public IActionResult ResetPassword(string id) => View(new ResetPasswordViewModel()
        {
            ActiveCode=id
        });
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if(!ModelState.IsValid)
            {
                return View(resetPassword);
            }

            User user = _userService.GetUserByActiveCode(resetPassword.ActiveCode);

            if(user==null)
            {
                return NotFound();
            }

            string hashNewPassword = PasswordHelper.EncodePasswordMd5(resetPassword.Password);
            user.Password = hashNewPassword;
            _userService.Update(user);

            return Redirect("/Login");
        }

        #endregion
    }
}
