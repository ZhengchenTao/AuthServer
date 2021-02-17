using AuthServer.Application.Users.Commands;
using AuthServer.Application.Users.Models;
using AuthServer.Application.Users.Queries;
using AuthServer.Security.Model;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Security
{
    public class APIAuthorizationHandler : IAuthorizationHandler
    {
        private readonly AsyncLock _lock = new AsyncLock();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public APIAuthorizationHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper, IMediator mediator)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var isAuthenticated = context.User.Identity.IsAuthenticated;

            if (isAuthenticated)
            {
                var user = await GetUserAsync(HttpUserModel.GenerateHttpUserModel(_httpContextAccessor));
                //TODO: Add user to service

                var pendingRequirements = context.PendingRequirements.ToList();
                //TODO: Add permission and Role filter 
            }
            await Task.CompletedTask;
        }

        private async Task<UserModel> GetUserAsync(HttpUserModel httpUser)
        {
            using (await _lock.LockAsync().ConfigureAwait(true))
            {
                var user = await _mediator.Send(new GetUserByIdentityQuery()
                {
                    Identity = httpUser.Identity,
                    Provider = httpUser.Provider
                });
                if (user == null)
                {
                    user = await _mediator.Send(new CreateUserByIdentityCommand()
                    {
                        User = _mapper.Map<UserModel>(httpUser)
                    });
                }
                return user;
            }
        }
    }
}
