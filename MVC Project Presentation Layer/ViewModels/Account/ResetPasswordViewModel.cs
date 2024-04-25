using System.ComponentModel.DataAnnotations;

namespace MVC_Project_Presentation_Layer.ViewModels.Account
{
    public class ResetPasswordViewModel
    {


        [Required(ErrorMessage = "new password is Required")]
        [MinLength(5, ErrorMessage = "at least 5 chars")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = " Confirming password is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "doesn't match")]
        public string ConfirmPassword { get; set; }
        //public string Email { get; set; }
        //public string Token { get; set; }


    }
}
