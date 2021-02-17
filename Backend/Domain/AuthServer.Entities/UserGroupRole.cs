using AuthServer.Entities.BaseEntities;
using System;

namespace AuthServer.Entities
{
    public class UserGroupRole : BaseEntity
    {
        public Guid UserGroupId { get; set; }

        public Guid RoleId { get; set; }

        public virtual UserGroup UserGroup { get; set; }

        public virtual Role Role { get; set; }
    }
}
