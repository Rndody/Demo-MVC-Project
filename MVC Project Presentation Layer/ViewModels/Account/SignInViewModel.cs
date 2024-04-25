using System.ComponentModel.DataAnnotations;

namespace MVC_Project_Presentation_Layer.ViewModels.Account
{
    public class SignInViewModel
    {

        [Required(ErrorMessage = "email is requierd")]
        [EmailAddress(ErrorMessage = "invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is Required")]     
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
