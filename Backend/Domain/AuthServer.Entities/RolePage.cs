using AuthServer.Entities.BaseEntities;
using System;

namespace AuthServer.Entities
{
    public class RolePage : BaseEntity
    {
        public Guid RoleId { get; set; }

        public Guid PageId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Page Page { get; set; }
    }
}
