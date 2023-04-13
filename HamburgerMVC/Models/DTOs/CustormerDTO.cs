using System.ComponentModel.DataAnnotations;
namespace HamburgerMVC.Models.DTOs
{
    public class CustormerDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
