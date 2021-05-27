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

namespace PhotoContest.Web
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

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await this.userService.GetAllAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Rank)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
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
                var user = await this.userService.CreateAsync(newUserDTO);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RankId"] = new SelectList(_context.Ranks, "Id", "Id", user.RankId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FirstName,LastName,RankId,OverallPoints,CreatedOn,ModifiedOn,DeletedOn,IsDeleted,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RankId"] = new SelectList(_context.Ranks, "Id", "Id", user.RankId);
            return View(user);
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
