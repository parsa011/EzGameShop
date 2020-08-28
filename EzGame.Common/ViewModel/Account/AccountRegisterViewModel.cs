using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EzGame.Common.ViewModel.Account
{
   public class AccountRegisterViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام کاربری")]
        [Remote("IsUserNameInUse", "Account", AdditionalFields = "__RequestVerificationToken", HttpMethod = "POST")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل را به درستی وارد نمایید")]  
        [Remote("IsEmailInUse", "Account",AdditionalFields = "__RequestVerificationToken",HttpMethod = "POST")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "رمز عبوز")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تکرار کلمه عبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage= "گذرواژه و تکرار ان با هم یکسان نیستند")]
        public string ConfirmPassword { get; set; }
    }
}
