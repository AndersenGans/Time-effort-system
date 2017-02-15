using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string PreviousUrl { get; set; }
    }
}