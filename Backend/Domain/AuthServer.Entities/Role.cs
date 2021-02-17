using AuthServer.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Entities
{
    public class Role : AuditEntity, ISoftDelete
    {
        [Required, MaxLength(20)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string DisplayName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsDeleted { get; set; }
    }
}
