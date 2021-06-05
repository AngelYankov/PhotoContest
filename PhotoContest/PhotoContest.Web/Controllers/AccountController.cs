using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Web.Models;
using PhotoContest.Web.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;
        private readonly IUserService userService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
            this.userService = userService;
        }

		/// <summary>
		/// Show form for login details
		/// </summary>
		[HttpGet]
		public IActionResult Login()
		{
			var model = new LoginViewModel();
			return View(model);
		}

		/// <summary>
		/// Sign in to account
		/// </summary>
		/// <param name="model">Login details put in the form</param>
		/// <returns>Redirects to home page if successful or to an error page if bad request</returns>
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
			}
			return RedirectToAction("Error", "Home");
		}

		/// <summary>
		/// Show form to put in details to register
		/// </summary>
		/*[HttpGet]
		public IActionResult Register()
		{
			var model = new RegisterViewModel();
			return View(model);
		}

		/// <summary>
		/// Confirm registration
		/// </summary>
		/// <param name="model">Details put in form to register</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new NewUserDTO();
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;
				user.Email = model.Email;
				user.EmailConfirmed = model.Email;
				user.Password = model.Password;

				var result = await this.userService.CreateAsync(user);
                *//*if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }*//*
            }

			return RedirectToAction("Error", "Home");
		}*/
	}
}
