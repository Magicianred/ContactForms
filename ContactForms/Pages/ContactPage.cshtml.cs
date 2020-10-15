using System.ComponentModel.DataAnnotations;
using ContactForms.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContactForms.Pages
{
    public class ContactPage : PageModel, IContactModel
    {
        private readonly ILogger<ContactPage> logger;

        public ContactPage(ILogger<ContactPage> logger)
        {
            this.logger = logger;
        }
        
        [UsedImplicitly]
        public IActionResult OnPost([FromServices] Database database)
        {
            if (ModelState.IsValid)
            {
                // map current data to model
                ContactModel model = new() {
                    Email = Email,
                    Message = Message,
                    Name = Name,
                    Subject = Subject,
                };
                
                database.Save(model);
                SuccessMessage = "Thank you for contacting us!";
                
                logger.LogInformation("Contact message submitted: {Message}", model);
                
               return RedirectToPage("ContactPage");
            }

            return Page();
        }

        [BindProperty, Required, Display(Name = "Full Name")]
        public string Name { get; init; }
        [BindProperty,Required, Display(Name = "Subject")]
        public string Subject { get; init; }
        [BindProperty,Required, EmailAddress, Display(Name = "Email Address")]
        public string Email { get; init; }
        [BindProperty, Required, Display(Name = "Message")]
        public string Message { get; init; }
        
        public bool HasSuccessMessage => SuccessMessage is not null;
        
        [TempData]
        public string SuccessMessage { get; set; }
    }
}