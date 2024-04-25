using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project_Data_Access_Layer.Models;
using MVC_Project_Presentation_Layer.ViewModels.Account;
using System.Threading.Tasks;

namespace MVC_Project_Presentation_Layer.Controllers
{
    public class AccountController : Controller
    {        //register//login//sign out //forget password //reset password
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
            if (ModelState.IsValid) //server side validation
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
                    if (result.Succeeded) return RedirectToAction(nameof(SignIn));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                ModelState.AddModelError(string.Empty, "this user name is taken");
            }
            return View(model);
        }
        #endregion


        #region Sign In
        public IActionResult SignIn() => View();

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var flag = await userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.IsLockedOut)
                            ModelState.AddModelError(string.Empty, "locked  account");
                        if (result.Succeeded)
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        if (result.IsNotAllowed)
                            ModelState.AddModelError(string.Empty, "not confirmed account");


                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(model);
        }
        #endregion

        #region Sign-Out

        public async new Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        #endregion


        #region Forget Password
        public IActionResult ForgetPassword() => View();


        [HttpPost]
        public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user= await  userManager.FindByEmailAsync(model.Email);
                if (user != null)
                { 

                }
                ModelState.AddModelError(string.Empty, "no such account here");
                    }
            return View(model);
        }

        #endregion
    }
}
