﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Display(Name ="First Name")]
        public string FirstName { get; set; } = null!;
		[Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;
		[Required]
        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Confirmed Passowrd Doesn't Match the Password")]
        public string ConfirmPassword { get; set; } = null!;

        public bool IsAgree { get; set; }
    }
}
