using Demo.DAL.Common.Email;
using Demo.DAL.Entities.Identity;
using Demo.PL.Helpers;
using Demo.PL.Settings;
using Demo.PL.ViewModels.Identity;
using Demo.PL.ViewModels.Users;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Demo.PL.Controllers
{
    public class AcountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IMailSettings _mailSettings;

		public AcountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMailSettings mailSettings)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_mailSettings = mailSettings;
		}
        #region SignUp

        [HttpGet] // GET: /Acount/SignUp 
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			// Check if the username already exists
			var existingUser = await _userManager.FindByNameAsync(model.UserName);
			if (existingUser is not null)
			{
				ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This username is already in use.");
				return View(model); // Early return to avoid unnecessary processing
			}

			// Create the new user object
			var user = new ApplicationUser
			{
				UserName = model.UserName,
				FName = model.FirstName,
				LName = model.LastName,
				Email = model.Email,
				IsAgree = model.IsAgree
			};

			// Attempt to create the user
			var result = await _userManager.CreateAsync(user, model.Password);

			// If successful, redirect to the sign-in page
			if (result.Succeeded)
				return RedirectToAction(nameof(SignIn));

			// Add any errors from the result to the model state
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			// Return the view with the model if errors occurred
			return View(model);
		}

		#endregion


		#region SignIn

		[HttpGet] // GET: /Acount/SignIn 
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user == null)
			{
				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
				return View(model);
			}

			var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);

			if (result.IsNotAllowed)
			{
				ModelState.AddModelError(string.Empty, "Your account is not confirmed yet.");
			}
			else if (result.IsLockedOut)
			{
				ModelState.AddModelError(string.Empty, "Your account is locked. Please try again later.");
			}
			else if (result.Succeeded)
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}

			else
			{
				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			}

			return View(model);
		}


		#endregion


		#region SignOut
		public async new Task<IActionResult>SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}

		#endregion

		#region ForgetPassword

		public IActionResult ForgetPassword()
		{
			return View();
		}


		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					// Create the token which is valid for only one time 
					var token = await _userManager.GeneratePasswordResetTokenAsync(User);
					// Create the URL for the ResetPassword
					var ResetPasswordLink = Url.Action("ResetPassword", "Acount", new {email = User.Email,Token = token },Request.Scheme);

					// Create Email
					var email = new Email
					{
						Subject = "Reset Password",
						To = model.Email,
						Body = ResetPasswordLink
					};
					// Send the Email
					 _mailSettings.sendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				else
				{
					ModelState.AddModelError(string.Empty, "User Is not Found");
				}
			}
				return View(nameof(ForgetPassword), model);
		}


		public IActionResult CheckYourInbox()
		{
			return View();
		}
		#endregion

		#region ResetPassword
		[HttpGet]
		public IActionResult ResetPassword(string email,string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				string email = TempData["email"] as string ?? string.Empty;
				string token = TempData["token"] as string ?? string.Empty;
				var User = await _userManager.FindByEmailAsync(email);
				var Result = await _userManager.ResetPasswordAsync(User, token, model.NewPassword);

				if (Result.Succeeded)
				{
					return RedirectToAction(nameof(SignIn));
				}
				else
				{
					foreach(var err in Result.Errors)
					{
						ModelState.AddModelError(string.Empty, err.Description);
					}
				}
			}
			return View(model);
		}

		#endregion

	}
}
