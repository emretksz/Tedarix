using Tedarix.Models;
using Tedarix.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tedarix.Areas.ManagementPanel.Controllers
{
    [Area("ManagementPanel")]
    [Authorize(Roles = "Kullanici")]
    public class TenantController : Controller
    {

        public async Task<IActionResult> Index()
        {
            using (TedarixContext db = new TedarixContext())
            {
                var tenants = await db.Tenants.ToListAsync();
                return View(tenants);
            }

        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Tenant tenant)
        {
            if (tenant == null)
            {
                return View(tenant);
            }
            using (TedarixContext db = new TedarixContext())
            {
                var tenants = await db.Tenants.AddAsync(tenant);
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
                var tenants = await db.Tenants.FirstOrDefaultAsync(a => a.Id == id);
                return View(tenants);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update(Tenant tenant)
        {
            using (TedarixContext db = new TedarixContext())
            {
                var tenants = db.Set<Tenant>().Update(tenant);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            using (TedarixContext db = new TedarixContext())
            {
                var tenants = await db.Tenants.FirstOrDefaultAsync(a => a.Id == id);
                db.Set<Tenant>().Remove(tenants);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }


    }
}
