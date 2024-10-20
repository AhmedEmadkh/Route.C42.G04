using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Users
{
	public class ResetPasswordViewModel
	{
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword),ErrorMessage ="Password Doesn't Match")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
