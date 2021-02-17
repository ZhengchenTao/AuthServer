using AuthServer.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Entities
{
    public class User : AuditEntity, ITenantId, ISoftDelete
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        public Guid? TenantId { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual IEnumerable<UserUserGroup> UserUserGroups { get; set; }

        public virtual IEnumerable<UserRole> UserRoles { get; set; }

        public virtual IEnumerable<UserIdentity> UserIdentities { get; set; }
    }
}
