using AuthServer.Entities.BaseEntities;
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Entities
{
    public class PageAction : BaseEntity
    {
        public Guid PageId { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public virtual Page Page { get; set; }
    }
}
