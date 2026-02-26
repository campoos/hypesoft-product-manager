using Hypesoft.Application.Commands.Produtos;
using Hypesoft.Application.DTOs.Produtos;
using Hypesoft.Application.Queries.Produtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var command = new CreateProdutoCommand(produtoRequest);
            var resultado = await _mediator.Send(command);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoResponseDto>>> GetAll()
        {
            var query = new GetAllProdutosQuery();
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoResponseDto>> GetById([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest(new { error = "formato de ID inválido" });

            var query = new GetByIdProdutosQuery(id);
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoResponseDto>> Update([FromRoute] string id, [FromBody] ProdutoRequestDto produtoRequest)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest(new { error = "formato de ID inválido" });

            var command = new UpdateProdutoCommand(produtoRequest, id);
            var resultado = await _mediator.Send(command);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest(new { error = "formato de ID inválido" });

            var command = new DeleteProdutoCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}