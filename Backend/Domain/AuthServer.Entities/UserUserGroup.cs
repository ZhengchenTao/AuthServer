using AuthServer.Entities.BaseEntities;
using System;

namespace AuthServer.Entities
{
    public class UserUserGroup : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid UserGroupId { get; set; }

        public virtual User User { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
