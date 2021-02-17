using AuthServer.Entities.BaseEntities;
using AuthServer.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Entities
{
    public class SystemConfiguration : BaseEntity
    {
        public SystemConfigurationType Type { get; set; }

        [Required, MaxLength(20)]
        public string Key { get; set; }

        [Required, MaxLength(200)]
        public string Value { get; set; }
    }
}
