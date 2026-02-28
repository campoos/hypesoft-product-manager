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

        /// <summary>
        /// Retorna dados consolidados para o dashboard
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna métricas gerais como:
        /// - Total de produtos
        /// - Total de categorias
        /// - Estatísticas de estoque
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(DashboardDataDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<DashboardDataDto>> Get()
        {
            var query = new GetDashboardDataQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}