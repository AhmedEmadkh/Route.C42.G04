using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AcountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AcountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
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


	}
}
