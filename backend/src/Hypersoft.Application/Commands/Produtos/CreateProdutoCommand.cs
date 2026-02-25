using Hypesoft.Application.DTOs;
using MediatR;

namespace Hypesoft.Application.Commands.Produtos
{
    public class CreateProdutoCommand : IRequest<ProdutoDto>
    {
        public ProdutoDto Produto { get; }

        public CreateProdutoCommand(ProdutoDto produto)
        {
            Produto = produto;
        }
    }
}