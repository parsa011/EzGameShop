using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EzGame.Common.ViewModel.Users
{
   public class UserEditViewModel
    {
        public string userId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل را به درستی وارد نمایید")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "رمز عبوز")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "گذرواژه و تکرار ان با هم یکسان نیستند")]
        public string ConfirmPassword { get; set; }
    }
}
