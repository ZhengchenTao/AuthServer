using AuthServer.Entities.BaseEntities;
using AuthServer.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Entities
{
    public class UserIdentity : BaseEntity, ISoftDelete
    {
        public Guid UserId { get; set; }

        [Required]
        public string Identity { get; set; }

        public IdentityProvider Provider { get; set; }

        public bool IsDeleted { get; set; }

        public virtual User User { get; set; }
    }
}
