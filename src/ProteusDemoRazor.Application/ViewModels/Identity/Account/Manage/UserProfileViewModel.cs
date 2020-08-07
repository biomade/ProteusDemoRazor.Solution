using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proteus.Application.ViewModels.Identity.Account.Manage
{
    public class UserProfileViewModel
    {
       public int Id { get; set; }
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

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
