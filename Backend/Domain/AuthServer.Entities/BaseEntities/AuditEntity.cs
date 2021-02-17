using System;

namespace AuthServer.Entities.BaseEntities
{
    public class AuditEntity : BaseEntity
    {
        public DateTime CreatedTime { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}
