using Tedarix.Models.Entities;
using Tedarix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Tedarix.Areas.ManagementPanel.Controllers
{
    [Authorize(Roles = "Kullanici")]
    public class CinsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (TedarixContext db = new TedarixContext())
            {
                var r = await db.Cinss.ToListAsync();
                return View(r);
            }

        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Cins cins)
        {
            if (cins == null)
            {
                return View(cins);
            }
            using (TedarixContext db = new TedarixContext())
            {
                var r = await db.Cinss.AddAsync(cins);
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
                var r = await db.Cinss.FirstOrDefaultAsync(a => a.Id == id);
                return View(r);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update(Cins cins)
        {
            using (TedarixContext db = new TedarixContext())
            {
                var r = db.Set<Cins>().Update(cins);
                await db.SaveChangesAsync();

                return View(r);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            using (TedarixContext db = new TedarixContext())
            {
                var r = await db.Cinss.FirstOrDefaultAsync(a => a.Id == id);
                db.Set<Cins>().Remove(r);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }
    }
}
