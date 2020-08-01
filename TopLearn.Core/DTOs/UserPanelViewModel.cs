using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TopLearn.Core.DTOs
{
    public class InformationUserViewModel
    {
        public string UserName { get; set; }
        public string  Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public double wallet { get; set; }
    }
    public class SideBarUserPanelViewModel
    {
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public string  ImageName { get; set; }
    }

    public class EditProfileViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [EmailAddress]
        public string Email { get; set; }

        public IFormFile UserAvatar { get; set; }
        public string AvatarName { get; set; }
    }
}
