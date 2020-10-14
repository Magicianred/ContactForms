using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContactForms.Models
{
    public record ContactModel : IContactModel
    {
        [Required, Display(Name = "Full Name")]
        public string Name { get; init; }
        [Required, Display(Name = "Subject")]
        public string Subject { get; init; }
        [Required, EmailAddress, Display(Name = "Email Address")]
        public string Email { get; init; }
        [Required, Display(Name = "Message")]
        public string Message { get; init; }
        public bool HasSuccessMessage => 
            SuccessMessage is not null;
        
        public string SuccessMessage { get; init; }

        public static IEnumerable<SelectListItem> Subjects { get; } 
            = new List<SelectListItem> {
            new ("Please select a subject...", null, true, true),
            new ("Question", "Question"),
            new ("Feedback", "Feedback"),
            new ("Praise", "Praise"),
            new ("Other", "Other"),
        }.AsReadOnly();
    }
}