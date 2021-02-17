using AuthServer.Entities.BaseEntities;
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Entities
{
    public class Module : BaseEntity
    {
        [Required, MaxLength(20)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string DisplayName { get; set; }

        [Range(0, 999)]
        public int Sequence { get; set; }
    }
}
