using Tedarix.Models.Entities;
using Tedarix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Tedarix.Areas.ManagementPanel.Controllers
{
    [Authorize(Roles = "Kullanici")]
    public class UserController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (TedarixContext db = new TedarixContext())
            {
                var users = await db.Users.ToListAsync();
                return View(users);
            }

        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (user == null)
            {
                return View(user);
            }
            using (TedarixContext db = new TedarixContext())
            {
                var Users = await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                return View(Users);
            }

        }
        public async Task<IActionResult> Update(long id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            using (TedarixContext db = new TedarixContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(a => a.Id == id);
                return View(user);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            using (TedarixContext db = new TedarixContext())
            {
                var users = db.Set<User>().Update(user);
                await db.SaveChangesAsync();

                return View(users);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            using (TedarixContext db = new TedarixContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(a => a.Id == id);
                db.Set<User>().Remove(user);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }
    }
}
