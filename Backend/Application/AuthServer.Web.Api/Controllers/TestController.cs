using AuthServer.Application.Users.Models;
using AuthServer.Application.Users.Queries;
using AuthServer.Enums;
using AuthServer.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<UserModel>> GetUser(string identity, IdentityProvider provider)
        {
            var query = new GetUserByIdentityQuery()
            {
                Identity = identity,
                Provider = provider
            };
            return await _mediator.Send(query);
        }

        [HttpGet("GetError")]
        public async Task<ActionResult<string>> GetError()
        {
            throw new FriendlyException(ErrorCode.CreateUserFailed);
        }
    }
}