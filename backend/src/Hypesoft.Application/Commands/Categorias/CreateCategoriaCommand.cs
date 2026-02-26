using Hypesoft.Application.DTOs.Categorias;
using MediatR;

namespace Hypesoft.Application.Commands.Categorias
{
    public class CreateCategoriaCommand : IRequest<CategoriaResponseDto>
    {
        public CategoriaRequestDto Categoria { get; }

        public CreateCategoriaCommand(CategoriaRequestDto categoria)
        {
            Categoria = categoria;
        }
    }
}