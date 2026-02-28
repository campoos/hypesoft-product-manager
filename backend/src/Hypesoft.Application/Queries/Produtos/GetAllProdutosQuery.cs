using Hypesoft.Application.DTOs.Produtos;
using MediatR;
using System.Collections.Generic;

namespace Hypesoft.Application.Queries.Produtos
{
    public class GetAllProdutosQuery : IRequest<List<ProdutoResponseDto>>
    {
        public string? Nome { get; set; }
        public string? CategoriaId { get; set; }
        public int? EstoqueMax { get; set; }
    }
}