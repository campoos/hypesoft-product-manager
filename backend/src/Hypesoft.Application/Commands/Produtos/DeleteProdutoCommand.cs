using MediatR;

namespace Hypesoft.Application.Commands.Produtos
{
    public class DeleteProdutoCommand : IRequest
    {
        public string Id { get; }

        public DeleteProdutoCommand(string id)
        {
            Id = id;
        }
    }
}