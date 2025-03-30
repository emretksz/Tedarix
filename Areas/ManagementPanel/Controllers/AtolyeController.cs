using Tedarix.Models.Entities;
using Tedarix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Tedarix.Areas.ManagementPanel.Controllers
{
    [Area("ManagementPanel")]
    [Authorize(Roles = "Kullanici")]
    public class AtolyeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (TedarixContext db = new TedarixContext())
            {
                var r = await db.Atolyes.ToListAsync();
                return View(r);
            }

        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Atolye atolye)
        {
            if (atolye == null)
            {
                return View(atolye);
            }
            using (TedarixContext db = new TedarixContext())
            {
                var r = await db.Atolyes.AddAsync(atolye);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
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
                var r = await db.Atolyes.FirstOrDefaultAsync(a => a.Id == id);
                return View(r);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update(Atolye atolye)
        {
            using (TedarixContext db = new TedarixContext())
            {
                var r = db.Set<Atolye>().Update(atolye);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            using (TedarixContext db = new TedarixContext())
            {
                var r = await db.Atolyes.FirstOrDefaultAsync(a => a.Id == id);
                db.Set<Atolye>().Remove(r);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }
    }
}
