using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using PhotoContest.Web.Models.UserViewModels;
using NToastNotify;

namespace PhotoContest.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IToastNotification toastNotification;

        public UsersController(IUserService userService,IToastNotification toastNotification)
        {
            this.userService = userService;
            this.toastNotification = toastNotification;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of those users</returns>
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Index()
        {
            var users = await this.userService.GetAllAsync();
            return View(users.Select(u=>new UserViewModel(u)));
        }

        /// <summary>
        /// Get all users participating i na contest
        /// </summary>
        /// <returns>List of those users</returns>
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> ViewAllParticipants()
        {
            var users = await this.userService.GetAllParticipantsAsync();
            return View(users.Select(u => new UserViewModel(u)));
        }

        /*public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newUserDTO = new NewUserDTO()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        EmailConfirmed = model.EmailConfirmed,
                        Password = model.Password
                    };
                    var user = await this.userService.CreateAsync(newUserDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return View();
        }*/

        /// <summary>
        /// Show form to create organizer
        /// </summary>
        [Authorize(Roles = "Admin")]
        public IActionResult CreateOrganizer()
        {
            return View();
        }

        /// <summary>
        /// Create organizer
        /// </summary>
        /// <param name="model">Details of the organizer</param>
        /// <returns>List of users or error page if bad request</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrganizer(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newUserDTO = new NewUserDTO()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        EmailConfirmed = model.EmailConfirmed,
                        Password = model.Password
                    };
                    var user = await this.userService.CreateOrganizerAsync(newUserDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    toastNotification.AddErrorToastMessage(e.Message, new NotyOptions());
                    return View();
                }
            }
            return View();
        }

        /// <summary>
        /// Get user to edit
        /// </summary>
        /// <param name="username">Username of the user</param>
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Edit(string username)
        {
            try
            {
                var user = await this.userService.GetUserByUsernameAsync(username);
                var userDTO = new UserDTO(user);
                return View(new EditUserViewModel(userDTO));
            }
            catch (Exception)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
        }

        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <param name="model">Details to update</param>
        /// <returns>List of all users or error page if bad request</returns>
        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string username, EditUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var updateUserDTO = new UpdateUserDTO()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                    };
                    await this.userService.UpdateAsync(updateUserDTO, username);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    toastNotification.AddErrorToastMessage(e.Message, new NotyOptions());
                    var path = Request.Path.Value.ToString() + "?Username=" + model.Username;
                    return Redirect(path);
                }
            }
            return View();
        }

        /// <summary>
        /// Get user to delete
        /// </summary>
        /// <param name="username">Username of the user</param>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string username)
        {
            try
            {
                var user = await this.userService.GetUserByUsernameAsync(username);
                var userDTO = new UserDTO(user);
                return View(new UserViewModel(userDTO));
            }
            catch (Exception)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <returns>List of all users or error page if bad request</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string username)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.userService.DeleteAsync(username);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get user by username
        /// </summary>
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult SearchByUsername()
        {
            return View();
        }

        /// <summary>
        /// Show details about user
        /// </summary>
        /// <param name="model">Username of the user</param>
        /// <returns>Details of the user or error page if bad request</returns>
        [Authorize(Roles = "Admin,Organizer,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowUserInfo(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await this.userService.GetUserByUsernameAsync(model.Username);
                    var userDTO = new UserDTO(user);
                    var userViewModel = new UserViewModel(userDTO);
                    return View(userViewModel);
                }
                catch (Exception)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }
            }
            return View();
        }

        /// <summary>
        /// Get details for signed in user
        /// </summary>
        [Authorize]
        public async Task<IActionResult> ShowMyAccountInfo()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userDTO = await this.userService.ShowMyAccountInfo();
                    var userViewModel = new UserViewModel(userDTO);
                    return View(userViewModel);
                }
                catch (Exception)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }
            }
            return View();
        }
    }
}
