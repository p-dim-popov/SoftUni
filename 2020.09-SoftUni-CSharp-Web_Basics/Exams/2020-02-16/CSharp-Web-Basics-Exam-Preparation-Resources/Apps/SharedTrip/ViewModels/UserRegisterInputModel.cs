using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTrip.ViewModels
{
    public class UserRegisterInputModel
    {
        [Required(ErrorMessage = 
            "Username" + ValidationHelper.RequiredErrorMessage)]
        [MinLength(5, ErrorMessage = 
            "Username" + ValidationHelper.MinLengthErrorMessage + "5")]
        [MaxLength(20, ErrorMessage = 
            "Username" + ValidationHelper.MaxLengthErrorMessage + "20")]
        public string Username { get; set; }

        [Required(ErrorMessage = 
            "Email" + ValidationHelper.RequiredErrorMessage)]
        [EmailAddress(ErrorMessage = 
            ValidationHelper.EmailErrorMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = 
            "Password" + ValidationHelper.RequiredErrorMessage)]
        [MinLength(6, ErrorMessage =
            "Password" + ValidationHelper.MinLengthErrorMessage + "6")]
        [MaxLength(20, ErrorMessage =
            "Password" + ValidationHelper.MaxLengthErrorMessage + "20")]
        public string Password { get; set; }
        
        [Compare(nameof(Password), ErrorMessage = 
            "Conirm password" + ValidationHelper.CompareErrorMessage + "password")]
        public string ConfirmPassword { get; set; }

    }
}
