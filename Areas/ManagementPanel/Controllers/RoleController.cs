using Tedarix.Models.Entities;
using Tedarix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Tedarix.Areas.ManagementPanel.Controllers
{
    [Authorize(Roles = "Kullanici")]
    public class RoleController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (TedarixContext db = new TedarixContext())
            {
                var role = await db.Roles.ToListAsync();
                return View(role);
            }

        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            if (role == null)
            {
                return View(role);
            }
            using (TedarixContext db = new TedarixContext())
            {
                var r = await db.Roles.AddAsync(role);
                await db.SaveChangesAsync();
                return View(r);
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
                var r = await db.Roles.FirstOrDefaultAsync(a => a.Id == id);
                return View(r);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update(Role role)
        {
            using (TedarixContext db = new TedarixContext())
            {
                var r = db.Set<Role>().Update(role);
                await db.SaveChangesAsync();

                return View(r);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            using (TedarixContext db = new TedarixContext())
            {
                var r = await db.Roles.FirstOrDefaultAsync(a => a.Id == id);
                db.Set<Role>().Remove(r);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }
    }
}
