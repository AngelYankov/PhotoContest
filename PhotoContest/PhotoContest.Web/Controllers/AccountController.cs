using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Data;
using PhotoContest.Web.Models;
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

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Login()
		{
			var model = new LoginViewModel();
			return View(model);
		}

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

		[HttpGet]
		public IActionResult Register()
		{
			var model = new RegisterViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new User();
				user.UserName = model.Email;
				user.Email = model.Email;
				user.PasswordHash = model.Password;

				var result = await this.userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
			}

			return RedirectToAction("Error", "Home");
		}
	}
}
