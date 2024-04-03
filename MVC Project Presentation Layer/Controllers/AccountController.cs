using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project_Data_Access_Layer.Models;
using MVC_Project_Presentation_Layer.ViewModels.User;
using System.Threading.Tasks;

namespace MVC_Project_Presentation_Layer.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}


		#region Sign-Up
		[HttpGet]
		public IActionResult SignUp() { return View(); }

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByNameAsync(model.Username);
				if (user == null)
				{
					user = new ApplicationUser()
					{
						FName = model.FirstName,
						LName = model.LastName,
						UserName = model.Username,
						Email = model.Email,
						IsAgree = model.IsAgree
					};
					var result = await userManager.CreateAsync(user, model.Password);
					if (result.Succeeded) { return RedirectToAction(nameof(SignIn)); }

					foreach(var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
				}
				ModelState.AddModelError(string.Empty, "this user name is taken");
			}
			return View(model);
		}
		#endregion


		#region Sign In
		public IActionResult SignIn() { return View(); }
		#endregion
	}
}
