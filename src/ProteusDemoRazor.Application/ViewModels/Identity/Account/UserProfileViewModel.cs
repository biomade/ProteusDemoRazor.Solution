using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proteus.Application.ViewModels.Identity.Account
{
    public class UserProfileViewModel
    {
       public int Id { get; set; }
        public string UserName { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string FirstName { get; set; }

        [MaxLength(1)]
        public string MI { get; set; }

        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[[0-9]{3}[-][0-9]{3}[-][0-9]{4}$", ErrorMessage = "The Phone pattern is XXX-YYY-ZZZZ.")]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string GovPOCName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string GovPOCEmail { get; set; }

        [Required]
        [RegularExpression(@"^[[0-9]{3}[-][0-9]{3}[-][0-9]{4}$", ErrorMessage = "The Phone pattern is XXX-YYY-ZZZZ.")]
        [Phone]
        public string GovPOCPhoneNumber { get; set; }

        public string EDI { get; set; }
    }
}
