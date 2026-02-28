
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.Exceptions;
using Hypesoft.Application.DTOs.Categorias;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hypesoft.Application.Queries.Categorias;
using AutoMapper;

namespace Hypesoft.Application.Handlers.Categorias
{
   public class GetByIdCategoriaHandler : IRequestHandler<GetByIdCategoriasQuery, CategoriaResponseDto>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public GetByIdCategoriaHandler(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<CategoriaResponseDto> Handle(GetByIdCategoriasQuery request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(request.Id);

            if (categoria == null)
                throw new NotFoundException("Categoria não encontrada.");

            return _mapper.Map<CategoriaResponseDto>(categoria);
        }
    }
}
