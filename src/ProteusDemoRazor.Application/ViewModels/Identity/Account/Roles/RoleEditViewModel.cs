using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proteus.Application.ViewModels.Identity.Account.Roles
{
    public class RoleEditViewModel
    {

        [Key, Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
