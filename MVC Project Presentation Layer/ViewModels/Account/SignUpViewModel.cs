using System.ComponentModel.DataAnnotations;

namespace MVC_Project_Presentation_Layer.ViewModels.Account
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "name is requierd")]
		public string Username { get; set; }
		[Required(ErrorMessage = "email is requierd")]
		[EmailAddress(ErrorMessage = "invalid email")]
		public string Email { get; set; }


		[Required(ErrorMessage = "First name is requierd")]
		[Display(Name = "First Name ")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last name is requierd")]
		[Display(Name = "Last Name ")]
		public string LastName { get; set; }




		[Required(ErrorMessage = "password is Required")]
		[MinLength(5, ErrorMessage ="at least 5 chars")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = " Confirming password is Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password) ,ErrorMessage ="doesn't match"  )]
		public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }

	}
}
