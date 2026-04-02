using System.ComponentModel.DataAnnotations;

namespace Portfoliowebsite.Models
{
    public class ContactFormModel
    {
        [Display(Name = "Naam")]
        [Required(ErrorMessage = "Vul je naam in.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Naam moet tussen 2 en 50 tekens zijn.")]
        public string Name { get; set; } = "";

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Vul je e-mailadres in.")]
        [EmailAddress(ErrorMessage = "Vul een geldig e-mailadres in.")]
        [StringLength(254, ErrorMessage = "E-mailadres is te lang.")]
        public string Email { get; set; } = "";

        [Display(Name = "Onderwerp")]
        [Required(ErrorMessage = "Vul een onderwerp in.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Onderwerp moet tussen 2 en 100 tekens zijn.")]
        public string Subject { get; set; } = "";

        [Display(Name = "Bericht")]
        [Required(ErrorMessage = "Vul een bericht in.")]
        [StringLength(2000, MinimumLength = 5, ErrorMessage = "Bericht moet tussen 5 en 2000 tekens zijn.")]
        public string Message { get; set; } = "";

        // Honeypot: echte gebruikers laten dit leeg (server-side checked)
        public string? Website { get; set; }
    }
}