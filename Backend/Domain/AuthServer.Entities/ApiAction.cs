using AuthServer.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Entities
{
    public class APIAction : BaseEntity
    {
        [Required, MaxLength(50)]
        public string ControllerName { get; set; }

        [Required, MaxLength(50)]
        public string ActionName { get; set; }
    }
}
