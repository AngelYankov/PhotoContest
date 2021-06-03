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

namespace PhotoContest.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Index()
        {
            var users = await this.userService.GetAllAsync();
            return View(users.Select(u=>new UserViewModel(u)));
        }

        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> ViewAllParticipants()
        {
            var users = await this.userService.GetAllParticipantsAsync();
            return View(users.Select(u => new UserViewModel(u)));
        }

        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Details(string username)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            var userDTO = new UserDTO(user);
            return View(new UserViewModel(userDTO));
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

        [Authorize(Roles = "Admin")]
        public IActionResult CreateOrganizer()
        {
            return View();
        }

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
                    return BadRequest(e.Message);
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Edit(string username)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            var userDTO = new UserDTO(user);
            return View(new EditUserViewModel(userDTO));
        }

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
                catch (Exception)
                {
                    return RedirectToAction("Error");
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string username)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            var userDTO = new UserDTO(user);
            return View(new UserViewModel(userDTO));
        }

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
                    return RedirectToAction("Error");
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult SearchByUsername()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Organizer")]
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
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return View();
        }
    }
}
