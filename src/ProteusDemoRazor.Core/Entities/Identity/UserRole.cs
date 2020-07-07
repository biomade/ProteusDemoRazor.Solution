using Proteus.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Proteus.Core.Entities.Identity
{
    //TODO IDENTITY: Step 1c - Create class that extends from Identity
    [Table("UserRole")]
    public class UserRole : Entity
    {
        [Required, ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required, ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        //for this userrole, get the user and the role info
        public Role Role { get; set; }

        public User User { get; set; }
    }
}
