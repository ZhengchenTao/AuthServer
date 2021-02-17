using AuthServer.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Entities
{
    public class Page : BaseEntity
    {
        public Guid ModuleId { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string DisplayName { get; set; }

        [Range(0, 999)]
        public int Sequence { get; set; }

        [MaxLength(20)]
        public string Icon { get; set; }

        public Guid? ParentPageId { get; set; }

        public virtual Module Module { get; set; }

        public virtual Page ParentPage { get; set; }

        public virtual IEnumerable<Page> ChildPage { get; set; }
    }
}
