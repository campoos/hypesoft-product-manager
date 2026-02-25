using Hypesoft.Application.DTOs.Produtos;
using MediatR;

namespace Hypesoft.Application.Commands.Produtos
{
    public class CreateProdutoCommand : IRequest<ProdutoResponseDto>
    {
        public ProdutoRequestDto Produto { get; }

        public CreateProdutoCommand(ProdutoRequestDto produto)
        {
            Produto = produto;
        }
    }
}