using AuthServer.Application.Roles.Commands;
using AuthServer.Application.Roles.Models;
using AuthServer.Application.Roles.Queries;
using AuthServer.Common.Extensions;
using AuthServer.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AuthServer.Web.Api.Controllers
{
    /// <summary>
    /// 角色
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="mediator"></param>
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PaginationList<RoleModel>>> Get([FromQuery]GetRoleListQuery query)
        {
            return (await _mediator.Send(query))
                .GetPaginationList(query.PagingInfo);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleModel>> Get(Guid id)
        {
            return await _mediator.Send(new GetRoleQuery(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<bool>> Post(CreateRoleCommand value)
        {
            return await _mediator.Send(value);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, UpdateRoleCommand value)
        {
            return await _mediator.Send(value.SetId(id));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteRoleCommand(id));
        }
    }
}