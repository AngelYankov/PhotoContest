using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;

namespace PhotoContest.Web.Controllers
{
    public class UController : Controller
    {
        private readonly PhotoContestContext _context;
        private readonly IUserService userService;

        public UController(PhotoContestContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.userService.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string username)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            return View(new UserDTO(user));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewUserDTO newUserDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await this.userService.CreateAsync(newUserDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return View();
        }
        public IActionResult CreateOrganizer()
        {
            return RedirectToAction("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrganizer(NewUserDTO newUserDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
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

        public async Task<IActionResult> Edit(string username)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            return View(new UserDTO(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string username, UpdateUserDTO updateUserDTO)
        {

            if (ModelState.IsValid)
            {
                try
                {
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

        public async Task<IActionResult> Delete(string username)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            return View(new UserDTO(user));
        }

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
    }
}
