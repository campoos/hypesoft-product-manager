using MediatR;

namespace Hypesoft.Application.Commands.Categorias
{
    public class DeleteCategoriaCommand : IRequest
    {
        public string Id { get; }

        public DeleteCategoriaCommand(string id)
        {
            Id = id;
        }
    }
}