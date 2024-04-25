using System.ComponentModel.DataAnnotations;

namespace MVC_Project_Presentation_Layer.ViewModels.Account
{
    public class ForgetPasswordViewModel
    {

        [Required(ErrorMessage = "email is requierd")]
        [EmailAddress(ErrorMessage = "invalid email")]
        public string Email { get; set; }
    }
}
