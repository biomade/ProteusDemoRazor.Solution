using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proteus.Application.ViewModels.Identity.Account
{
    public class LoginViewModel
    {
        //modified to only have the checkbox for Cert login, no username/pwd
        [Required]
        public bool DODAccept { get; set; }
    }
}
