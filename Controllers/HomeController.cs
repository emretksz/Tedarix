using Tedarix.Areas.ManagementPanel.Models;
using Tedarix.Models;
using Tedarix.Models.Entities;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace Tedarix.Controllers
{
    [Authorize(Roles = "Kullanici")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {


           List<FoyDtoIndex> dto = new List<FoyDtoIndex>();
            

      
            using (TedarixContext db = new TedarixContext())
            {
                ViewBag.YapilanFoy = await db.Foys.CountAsync();

                var foy = await db.Foys.Include(a => a.Tenant).Include(a => a.Atolye).Where(a=>a.ArsivlendiMi==false).AsNoTracking().ToListAsync();
                ViewBag.TamamlanmayanFoyler = foy.Count();
                foreach (var item in foy)
                {
                    FoyDtoIndex foys = new FoyDtoIndex();
                    foys.Foy= item;
                    foys.FoyAndCins = new List<Models.Entities.FoyAndCinsDto>();
                    foys.FoyAndKesim = new List<Models.Entities.FoyAndKesimDto>();

                    var cinsDto = await db.FoyAndCinsS.Include(a=>a.Cins).Where(a=>a.FoyId==item.Id&&a.State==false).AsNoTracking().ToListAsync();
                    foreach (var cins in cinsDto)
                    {
                        DateTime suAn = DateTime.Now;
                        TimeSpan gecenSure = suAn - item.RegisterDate;

                        foys.FoyAndCins.Add(new() { CinsAdi=cins.Cins.Name,RegisterDate= item.RegisterDate,GecenGun= gecenSure });
                    }

                    var kesimDto = await db.FoyAndKesims.Include(a=>a.Kesim).Where(a => a.FoyId == item.Id && a.State == false).AsNoTracking().ToListAsync();
                    foreach (var kesim in kesimDto)
                    {
                        DateTime suAn = DateTime.Now;
                        TimeSpan gecenSure = suAn - item.RegisterDate;

                        foys.FoyAndKesim.Add(new() { KesimAdi=kesim.Kesim.Name,RegisterDate=item.RegisterDate,GecenGun= gecenSure });
                    }

                    dto.Add(foys);
                }
     
            }
            return View(dto);
        } 
        public async Task<IActionResult> Foy()
        {
            
            using (TedarixContext db = new TedarixContext())
            {
                var foy = await db.Foys.ToListAsync();
                return View(foy);
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddFoy()
        {
            using (TedarixContext db = new TedarixContext())
            {
                ViewBag.Tenant = await db.Tenants.ToListAsync();
                ViewBag.Cins = await db.Cinss.ToListAsync();
                ViewBag.Kesim = await db.Kesims.ToListAsync();
                ViewBag.Atolye = await db.Atolyes.ToListAsync();

            }
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> AddFoy()
        //{

        //    using (TedarixContext db = new TedarixContext())
        //    {
        //        var foy = await db.Foys.ToListAsync();
        //        return View(foy);
        //    }
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}