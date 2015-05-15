using System;
using System.ComponentModel.DataAnnotations;
using klukk_social.Services;

namespace klukk_social.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Lykilorðið {0} þarf að vera amk {2} að lengd", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Nýtt lykilorð passar ekki við staðfesingarlykilorðið")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Aðalnafn")]
        public string First { get; set; }


        [Display(Name = "Millinafn")]
        public string Middle { get; set; }

        [Required]
        [Display(Name = "Eftirnafn")]
        public string Last { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Lykilorðið {0} þarf að vera amk {2} stafir að lengd", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Staðfesta lykilorð")]
        [Compare("Password", ErrorMessage = "Nýtt lykilorð passar ekki við staðfesingarlykilorðið")]
        public string ConfirmPassword { get; set; }

		public DateTime BirthDay { get; set; }
	}
	public class CreateChildViewModel
	{
		[Required]
		[Display(Name = "Aðalnafn")]
		public string First { get; set; }


		[Display(Name = "Millinafn")]
		public string Middle { get; set; }

		[Required]
		[Display(Name = "Eftirnafn")]
		public string Last { get; set; }

		[Required]
		[Display(Name = "Notendanafn")]
		public string UserName { get; set; }

		[Required]
        [StringLength(100, ErrorMessage = "Lykilorðið {0} þarf að vera amk {2} stafir að lengd", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Lykilorð")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Staðfesta lykilorð")]
        [Compare("Password", ErrorMessage = "Nýtt lykilorð passar ekki við staðfesingarlykilorðið")]
		public string ConfirmPassword { get; set; }
		
		[Required]
        [CustomValidation(typeof(DateAttribute), "Barn verður að vera á aldrinum 6 til 13 ára")]
		[Display(Name = "Afmælisdagur")]
		public DateTime BirthDay { get; set; }

	}

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Lykilorðið {0} þarf að vera amk {2} stafir að lengd", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Nýtt lykilorð passar ekki við staðfesingarlykilorðið")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class DateAttribute : RangeAttribute
    {
        public DateAttribute()
            : base(typeof(DateTime), DateTime.Now.AddYears(-13).ToShortDateString(), DateTime.Now.AddYears(-6).ToShortDateString()) { }
    }
}
