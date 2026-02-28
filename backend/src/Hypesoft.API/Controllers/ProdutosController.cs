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

        [HttpPost]
        public async Task<ActionResult<ProdutoResponseDto>> Create([FromBody] ProdutoRequestDto produtoRequest)
        {
            if (!ObjectId.TryParse(produtoRequest.CategoriaId, out _))
                throw new DomainValidationException("formato de categoriaId inválido.");

            var command = new CreateProdutoCommand(produtoRequest);
            var resultado = await _mediator.Send(command);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoResponseDto>>> GetAll(
            [FromQuery] string? nome,
            [FromQuery] string? categoriaId,
            [FromQuery] int? estoqueMax
        )
        {
            if (!string.IsNullOrEmpty(categoriaId))
            {
                if (!ObjectId.TryParse(categoriaId, out _))
                    throw new DomainValidationException("formato de ID inválido.");
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoResponseDto>> GetById([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out _))
                throw new DomainValidationException("formato de ID inválido.");

            var query = new GetByIdProdutosQuery(id);
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoResponseDto>> Update([FromRoute] string id, [FromBody] ProdutoRequestDto produtoRequest)
        {
            if (!ObjectId.TryParse(id, out _))
                throw new DomainValidationException("formato de ID inválido.");

            var command = new UpdateProdutoCommand(produtoRequest, id);
            var resultado = await _mediator.Send(command);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out _))
                throw new DomainValidationException("formato de ID inválido.");

            var command = new DeleteProdutoCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}