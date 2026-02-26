using Hypesoft.Application.DTOs.Produtos;
using MediatR;

namespace Hypesoft.Application.Queries.Produtos
{
    public class GetByIdProdutosQuery : IRequest<ProdutoResponseDto>
    {
        public string Id { get; }

        public GetByIdProdutosQuery(string id)
        {
            Id = id;
        }
    }
}