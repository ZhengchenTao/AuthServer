using System;

namespace AuthServer.Entities.BaseEntities
{
    public interface ITenantId
    {
        Guid? TenantId { get; set; }
    }
}
