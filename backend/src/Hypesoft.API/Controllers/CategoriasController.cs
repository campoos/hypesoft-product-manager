using Hypesoft.Application.Commands.Categorias;
using Hypesoft.Application.DTOs.Categorias;
//using Hypesoft.Application.Queries.Produtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hypesoft.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaResponseDto>> Create([FromBody] CategoriaRequestDto categoriaRequest)
        {
            var command = new CreateCategoriaCommand(categoriaRequest);
            var resultado = await _mediator.Send(command);
            return Ok(resultado);
        }
    }
}