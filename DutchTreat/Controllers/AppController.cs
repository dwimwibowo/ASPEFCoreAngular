using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DutchTreat.Services;
using DutchTreat.ViewModels;
using DutchTreat.Data;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly DutchContext _context;

        // Constructor
        public AppController(
                IMailService mailService,
                DutchContext context
        )
        {
            this._mailService = mailService;
            this._context = context;
        }

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
            //var results = _context.Products
            //    .OrderBy(p => p.Category).ThenBy(q => q.Id)
            //    .ToList();

            var results = from p in _context.Products
                          orderby p.Category, p.Id
                          select p;

            return View(results.ToList());
        }
    }
}
