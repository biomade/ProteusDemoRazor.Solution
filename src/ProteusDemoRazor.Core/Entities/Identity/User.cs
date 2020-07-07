using Proteus.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Proteus.Core.Entities.Identity
{
    //TODO IDENTITY: Step 1a - Create class that extends from Identity
    [Table("User")]
    public class User : Entity
    {        

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(1024)]
        public string PasswordHash { get; set; }

        [Required, MaxLength(128)]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MI { get; set; }
        public string LastName { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string NormalizedUserName { get; set; }
        public string NormalizedEmail { get; set; }

        [ForeignKey("Id")]
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
