using Proteus.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Proteus.Core.Entities.Identity
{
    //TODO IDENTITY: Step 1b - Create class that extends from Identity
    [Table("Role")]
    public class Role : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string NormalizedName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
