using Hypesoft.Application.Commands.Categorias;
using Hypesoft.Application.DTOs.Categorias;
using Hypesoft.Application.Queries.Categorias;
using Hypesoft.Domain.Exceptions;
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

        /// <summary>
        /// Cria uma nova categoria
        /// </summary>
        /// <remarks>
        /// Exemplo:
        /// {
        ///   "nome": "Eletrônicos"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(CategoriaResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaResponseDto>> Create([FromBody] CategoriaRequestDto categoriaRequest)
        {
            var command = new CreateCategoriaCommand(categoriaRequest);
            var resultado = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
        }

        /// <summary>
        /// Lista todas as categorias
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoriaResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoriaResponseDto>>> GetAll()
        {
            var query = new GetAllCategoriasQuery();
            var resultado = await _mediator.Send(query);

            return Ok(resultado);
        }

        /// <summary>
        /// Busca uma categoria pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoriaResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoriaResponseDto>> GetById([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out _))
                throw new DomainValidationException("Formato de ID inválido.");

            var query = new GetByIdCategoriasQuery(id);
            var resultado = await _mediator.Send(query);

            return Ok(resultado);
        }

        /// <summary>
        /// Atualiza uma categoria existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CategoriaResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoriaResponseDto>> Update(
            [FromRoute] string id,
            [FromBody] CategoriaRequestDto categoriaRequest)
        {
            if (!ObjectId.TryParse(id, out _))
                throw new DomainValidationException("Formato de ID inválido.");

            var command = new UpdateCategoriaCommand(categoriaRequest, id);
            var resultado = await _mediator.Send(command);

            return Ok(resultado);
        }

        /// <summary>
        /// Remove uma categoria
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out _))
                throw new DomainValidationException("Formato de ID inválido.");

            var command = new DeleteCategoriaCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}