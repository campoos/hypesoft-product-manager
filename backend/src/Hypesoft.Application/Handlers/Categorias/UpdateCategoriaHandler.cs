
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.Exceptions;
using Hypesoft.Application.DTOs.Categorias;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hypesoft.Application.Commands.Categorias;

namespace Hypesoft.Application.Handlers.Categorias
{
   public class UpdateCategoriaHandler : IRequestHandler<UpdateCategoriaCommand, CategoriaResponseDto>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public UpdateCategoriaHandler(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<CategoriaResponseDto> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoriaEncontrada = await _categoriaRepository.GetByIdAsync(request.Id);

            if (categoriaEncontrada == null)
                throw new NotFoundException("Categoria não encontrada.");

            categoriaEncontrada.Nome = request.Categoria.Nome;

            var atualizada = await _categoriaRepository.UpdateAsync(categoriaEncontrada);

            return _mapper.Map<CategoriaResponseDto>(atualizada);
        }
    }
}
