using Hypesoft.Application.DTOs.Categorias;
using MediatR;

namespace Hypesoft.Application.Commands.Categorias
{
    public class UpdateCategoriaCommand : IRequest<CategoriaResponseDto>
    {
        public CategoriaRequestDto Categoria { get; }
        public string Id { get; }

        public UpdateCategoriaCommand(CategoriaRequestDto categoria, string id)
        {
            Categoria = categoria;
            Id = id;
        }
    }
}