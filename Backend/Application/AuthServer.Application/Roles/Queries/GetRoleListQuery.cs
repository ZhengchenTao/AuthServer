using AuthServer.Application.Roles.Models;
using AuthServer.Common.Models;
using MediatR;
using System.Collections.Generic;

namespace AuthServer.Application.Roles.Queries
{
    public class GetRoleListQuery : IRequest<List<RoleModel>>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public SortingInfo SortingInfo { get; set; }
    }
}
