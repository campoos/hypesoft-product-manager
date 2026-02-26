using Hypesoft.Application.DTOs.Produtos;
using MediatR;

namespace Hypesoft.Application.Commands.Produtos
{
    public class UpdateProdutoCommand : IRequest<ProdutoResponseDto>
    {
        public ProdutoRequestDto Produto { get; }
        public string Id { get; }

        public UpdateProdutoCommand(ProdutoRequestDto produto, string id)
        {
            Produto = produto;
            Id = id;
        }
    }
}