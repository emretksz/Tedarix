using Tedarix.Areas.ManagementPanel.Helpers.EmailHelper;
using Tedarix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;

namespace Tedarix.Areas.ManagementPanel.Controllers
{
    public class EmailServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly EmailConfigService _emailService;

        public EmailServiceController(EmailConfigService emailService)
        {
            _emailService = emailService;
        }

        //public async Task<IActionResult> SendEmail(int foyId)
        //{
        //    string toEmail = "recipient@example.com";
        //    string subject = "Test Email";
        //    string htmlMessage = "<p>This is a test email.</p>";

        //    using (TedarixContext db= new TedarixContext())
        //    {
        //        var foy = db.Foys.FirstOrDefaultAsync(a=>a.Id==foyId);



        //        if (foy!=null)
        //        {
        //            await _emailService.SendEmailAsync(toEmail, subject, htmlMessage);
        //        }
        //    }
    

        //    return View();
        //}
    }
}
