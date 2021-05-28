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
    public class UsersController : Controller
    {
        private readonly PhotoContestContext _context;
        private readonly IUserService userService;

        public UsersController(PhotoContestContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await this.userService.GetAllAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string username)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            return View(new UserDTO(user));
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string username)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            return View(new UserDTO(user));
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string username)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            return View(new UserDTO(user));
        }

        // POST: Users/Delete/5
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

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
