using Hypesoft.Application.Commands.Categorias;
using Hypesoft.Application.DTOs.Categorias;
using Hypesoft.Application.Queries.Categorias;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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

        [HttpGet]
        public async Task<ActionResult<List<CategoriaResponseDto>>> GetAll()
        {
            var query = new GetAllCategoriasQuery();
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaResponseDto>> GetById([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest(new { error = "formato de ID inválido" });

            var query = new GetByIdCategoriasQuery(id);
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaResponseDto>> Update([FromRoute] string id, [FromBody] CategoriaRequestDto categoriaRequest)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest(new { error = "formato de ID inválido" });

            var command = new UpdateCategoriaCommand(categoriaRequest, id);
            var resultado = await _mediator.Send(command);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest(new { error = "formato de ID inválido" });

            var command = new DeleteCategoriaCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}