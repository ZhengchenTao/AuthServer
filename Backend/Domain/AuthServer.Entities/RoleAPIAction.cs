using AuthServer.Entities.BaseEntities;
using System;

namespace AuthServer.Entities
{
    public class RoleAPIAction : BaseEntity
    {
        public Guid RoleId { get; set; }

        public Guid APIActionId { get; set; }

        public virtual Role Role { get; set; }

        public virtual APIAction APIAction { get; set; }
    }
}
