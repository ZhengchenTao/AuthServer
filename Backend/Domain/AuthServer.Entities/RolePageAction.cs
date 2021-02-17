using AuthServer.Entities.BaseEntities;
using System;

namespace AuthServer.Entities
{
    public class RolePageAction : BaseEntity
    {
        public Guid RoleId { get; set; }

        public Guid PageActionId { get; set; }

        public virtual Role Role { get; set; }

        public virtual PageAction PageAction { get; set; }
    }
}
