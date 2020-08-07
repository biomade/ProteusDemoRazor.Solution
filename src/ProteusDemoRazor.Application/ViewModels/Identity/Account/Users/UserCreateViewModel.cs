using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proteus.Application.ViewModels.Identity.Account.Users
{
   public class UserCreateViewModel
    {
        
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"^[[0-9]{3}[-][0-9]{3}[-][0-9]{4}$", ErrorMessage = "The Phone pattern is XXX-YYY-ZZZZ.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }


        [Required]
        [RegularExpression(@"^[[0-9]{10}$", ErrorMessage = "The EDI must be 10 numbers.")]
        public string EDI { get; set; }

        public string FirstName { get; set; }

        public string MI { get; set; }

        public string LastName { get; set; }

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

        public bool IsEnabled { get; set; }

        public bool IsLockedOut { get; set; }


        public DateTime LastLoginDate { get; set; }

    }
}
