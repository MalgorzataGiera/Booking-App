using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Models;
using System.Security.Claims;

namespace ReservationApp.Controllers
{
    /// <summary>
    /// Controller for handling user accounts, including registration, login, and logout.
    /// </summary>
    public class AccountController : Controller
    {
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign-in manager.</param>
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

        /// <summary>
        /// GET action for user registration.
        /// </summary>
        /// <returns>The registration view.</returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// POST action for user registration.
        /// </summary>
        /// <param name="model">The registration view model.</param>
        /// <returns>If successful, redirects to the home page; otherwise, returns the registration view with errors.</returns>
        [HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if(ModelState.IsValid)
			{
				var user = new IdentityUser { UserName = model.Email, Email = model.Email };

				user.EmailConfirmed = true;

				var result = await userManager.CreateAsync(user, model.Password);

				if(result.Succeeded)
				{
					await signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("index", "home");
				}

				foreach(var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(model);
		}

        /// <summary>
        /// POST action for user logout.
        /// </summary>
        /// <returns>Redirects to the home page after logging out.</returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
			await signInManager.SignOutAsync();
			return RedirectToAction("index", "home");
        }

        /// <summary>
        /// GET action for user login.
        /// </summary>
        /// <returns>The login view.</returns>
        [HttpGet]
		public IActionResult Login()
		{
			return View();
		}

        /// <summary>
        /// POST action for user login.
        /// </summary>
        /// <param name="model">The login view model.</param>
        /// <returns>If successful, redirects to the home page; otherwise, returns the login view with errors.</returns>
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

				if (result.Succeeded)
				{
					// dodaje claim
                    IdentityUser user = await userManager.FindByNameAsync(model.Email);
                    var claims = await userManager.GetClaimsAsync(user);

                    return RedirectToAction("index", "home");
				}

				ModelState.AddModelError(string.Empty, "Logowanie nie powiodło się!");
				
				
				
			}
			return View(model);
		}
	}
}
