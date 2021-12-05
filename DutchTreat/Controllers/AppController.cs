using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DutchTreat.Services;
using DutchTreat.ViewModels;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;

        // Constructor
        public AppController(IMailService mailService)
        {
            this._mailService = mailService;
        }

        //
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Send Email
                this._mailService.SendMessage("dwimwibowo@gmail.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");

                ViewBag.UserMessage = "<div class=\"alert alert-default-info\" role=\"alert\">Mail Sent</div>";
            }
            else
            {
                // Show Error
            }

            return View();
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet("shop")]
        public IActionResult Shop()
        {
            return View();
        }
    }
}
