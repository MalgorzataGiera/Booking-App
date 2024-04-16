using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Models;
using System.Data;
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
        /// <returns>If successful, redirects to the admin page; otherwise, returns the registration view with errors.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };

                user.EmailConfirmed = true;

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("create", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        /// <summary>
        /// POST action for user logout.
        /// </summary>
        /// <returns>Redirects to the admin page after logging out.</returns>
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
        /// <returns>If successful, redirects to the admin page; otherwise, returns the login view with errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    IdentityUser user = await userManager.FindByNameAsync(model.Email);
                    var claims = await userManager.GetClaimsAsync(user);

                    return RedirectToAction("create", "home");
                }

                ModelState.AddModelError(string.Empty, "Login failed.");

            }
            return View(model);
        }

        /// <summary>
        /// Displays the change password form for the specified user.
        /// </summary>
        /// <param name="id">The ID of the user to change password for.</param>
        /// <returns>If successful, redirects to the users list page; otherwise, returns the change password view with errors.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        /// <summary>
        /// Changes the password for the specified user.
        /// </summary>
        /// <param name="model">The model containing user details.</param>
        /// <param name="newPassword">The new password for the user.</param>
        /// <returns>The action result.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(IdentityUser model, string newPassword)
        {
            if (String.IsNullOrEmpty(newPassword))
            {
            }
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var result = await userManager.ResetPasswordAsync(user, token, newPassword);

                if (result.Succeeded)
                    return RedirectToAction("ListUsers", "admin");
            }

            return View(model);
        }

        /// <summary>
        /// Displays the delete user confirmation page.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The delete user confirmation view.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View(user);
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>>Redirects to the users list after successful deletion.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var user = await userManager.FindByIdAsync(id);          
            if(!await userManager.IsInRoleAsync(user, "Admin"))
                await userManager.DeleteAsync(user);

            return RedirectToAction("ListUsers", "admin");                       
            
        }
    }
}
