﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proteus.Application.ViewModels.Identity.Account
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "User Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} long.", MinimumLength = 4)]
        public string Name { get; set; }

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
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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

    }
}
