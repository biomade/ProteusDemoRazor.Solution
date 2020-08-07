using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proteus.Application.ViewModels.Identity.Account
{
    public class RoleEditViewModel
    {

        [Key, Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
