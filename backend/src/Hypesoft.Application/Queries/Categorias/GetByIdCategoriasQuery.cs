using Hypesoft.Application.DTOs.Categorias;
using MediatR;

namespace Hypesoft.Application.Queries.Categorias
{
    public class GetByIdCategoriasQuery : IRequest<CategoriaResponseDto>
    {
        public string Id { get; }

        public GetByIdCategoriasQuery(string id)
        {
            Id = id;
        }
    }
}