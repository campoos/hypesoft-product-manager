using Hypesoft.Application.DTOs.Dashboard;
using Hypesoft.Application.Queries.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hypesoft.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardDataDto>> Get()
        {
            var query = new GetDashboardDataQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}