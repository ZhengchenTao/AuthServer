using AuthServer.Application.Roles.Models;
using AuthServer.Common.Extensions;
using AuthServer.Entities;
using AuthServer.Persistence.Repository;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthServer.Application.Roles.Queries
{
    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, List<RoleModel>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetRoleListQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<RoleModel>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var queryable = _repository.GetQueryable<Role>();

            if (!string.IsNullOrWhiteSpace(request.Name))
                queryable = queryable.Where(x => x.Name.Contains(request.Name));

            if (!string.IsNullOrWhiteSpace(request.DisplayName))
                queryable = queryable.Where(x => x.Name.Contains(request.DisplayName));

            var list = await queryable
                .DataSorting(request.SortingInfo)
                .DataPaging(request.PagingInfo).ToListAsync();

            return _mapper.Map<List<RoleModel>>(list);
        }
    }
}
