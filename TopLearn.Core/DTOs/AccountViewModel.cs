using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.Core.DTOs
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل را درست وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [Compare("Password",ErrorMessage ="کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }
    public class LoginViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل را درست وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
