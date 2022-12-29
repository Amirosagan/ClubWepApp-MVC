using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RunGroopsWebSite.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Email is required.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
