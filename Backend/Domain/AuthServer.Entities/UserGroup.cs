using AuthServer.Entities.BaseEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Entities
{
    public class UserGroup : AuditEntity, ISoftDelete
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsDeleted { get; set; }

        public virtual IEnumerable<UserUserGroup> UserUserGroups { get; set; }
    }
}
