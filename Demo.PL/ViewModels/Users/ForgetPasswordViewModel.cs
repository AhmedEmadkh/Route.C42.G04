using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Users
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
