using Hypesoft.Application.DTOs.Categorias;
using MediatR;
using System.Collections.Generic;

namespace Hypesoft.Application.Queries.Categorias
{
    public class GetAllCategoriasQuery : IRequest<List<CategoriaResponseDto>>
    {

    }
}