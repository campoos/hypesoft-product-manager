using Hypesoft.Application.Commands.Produtos;
using Hypesoft.Application.DTOs.Produtos;
using Hypesoft.Application.Queries.Produtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Hypesoft.Domain.Exceptions;

namespace Hypesoft.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdutosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo produto
        /// </summary>
        /// <remarks>
        /// Exemplo:
        /// {
        ///   "nome": "Notebook Gamer",
        ///   "categoriaId": "64f1c2e8a123456789abcd12",
        ///   "preco": 4500,
        ///   "estoque": 10
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProdutoResponseDto>> Create([FromBody] ProdutoRequestDto produtoRequest)
        {
            if (!ObjectId.TryParse(produtoRequest.CategoriaId, out _))
                throw new DomainValidationException("Formato de categoriaId inválido.");

            var command = new CreateProdutoCommand(produtoRequest);
            var resultado = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
        }

        /// <summary>
        /// Lista produtos com filtros opcionais
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProdutoResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProdutoResponseDto>>> GetAll(
            [FromQuery] string? nome,
            [FromQuery] string? categoriaId,
            [FromQuery] int? estoqueMax
        )
        {
            if (!string.IsNullOrEmpty(categoriaId))
            {
                if (!ObjectId.TryParse(categoriaId, out _))
                    throw new DomainValidationException("Formato de ID inválido.");
            }

            if (estoqueMax.HasValue && estoqueMax < 0)
                throw new DomainValidationException("EstoqueMax não pode ser negativo.");

            var query = new GetAllProdutosQuery
            {
                Nome = nome,
                CategoriaId = categoriaId,
                EstoqueMax = estoqueMax,
            };

            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca um produto pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProdutoResponseDto>> GetById([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out _))
                throw new DomainValidationException("Formato de ID inválido.");

            var query = new GetByIdProdutosQuery(id);
            var resultado = await _mediator.Send(query);

            return Ok(resultado);
        }

        /// <summary>
        /// Atualiza um produto existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProdutoResponseDto>> Update(
            [FromRoute] string id,
            [FromBody] ProdutoRequestDto produtoRequest)
        {
            if (!ObjectId.TryParse(id, out _))
                throw new DomainValidationException("Formato de ID inválido.");

            var command = new UpdateProdutoCommand(produtoRequest, id);
            var resultado = await _mediator.Send(command);

            return Ok(resultado);
        }

        /// <summary>
        /// Remove um produto
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out _))
                throw new DomainValidationException("Formato de ID inválido.");

            var command = new DeleteProdutoCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}