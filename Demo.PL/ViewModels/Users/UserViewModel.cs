using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Users
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;
        [Display(Name ="First Name")]
        public string Fname { get; set; } = null!;
        [Display(Name = "Last Name")]
        public string LName { get; set; } = null!;
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;
        public IEnumerable<string>? Roles { get; set; }
    }
}
