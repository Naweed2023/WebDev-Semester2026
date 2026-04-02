using Microsoft.AspNetCore.Mvc;
using Portfoliowebsite.Models;
using Portfoliowebsite.Services;

namespace Portfoliowebsite.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailSender _email;

        public ContactController(IEmailSender email)
        {
            _email = email;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Show empty form
            return View(new ContactFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactFormModel model)
        {
            // Server-side honeypot (spam bot check)
            if (!string.IsNullOrWhiteSpace(model.Website))
            {
                // behave like success to bots (don’t give hints)
                return RedirectToAction(nameof(Thanks));
            }

            // Server-side validation (FR04)
            if (!ModelState.IsValid)
            {
                // Return same view with model  input stays + error messages show
                return View(model);
            }

            // Minimal cleanup 
            model.Name = model.Name.Trim();
            model.Email = model.Email.Trim();
            model.Subject = model.Subject.Trim();
            model.Message = model.Message.Trim();

            try
            {
                await _email.SendAsync(model.Name, model.Email, model.Subject, model.Message);
            }
            catch
            {
                // Friendly message, no technical details FR04 + robustness
                ModelState.AddModelError(string.Empty, "Verzenden is momenteel niet mogelijk. Probeer het later opnieuw.");
                return View(model);
            }

            // Confirmation page values
            TempData["ThanksName"] = model.Name;
            TempData["ThanksEmail"] = model.Email;
            TempData["ThanksMessage"] = model.Message;

            // FR03: clear confirmation after successful send
            return RedirectToAction(nameof(Thanks));
        }

        [HttpGet]
        public IActionResult Thanks()
        {
            return View();
        }
    }
}