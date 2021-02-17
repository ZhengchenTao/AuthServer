using System;

namespace AuthServer.Application.BaseModel
{
    public class AuditModel
    {
        public DateTime CreatedTime { get; set; }

        public Guid CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public Guid? ModifiedBy { get; set; }

        public string ModifiedByName { get; set; }
    }
}
