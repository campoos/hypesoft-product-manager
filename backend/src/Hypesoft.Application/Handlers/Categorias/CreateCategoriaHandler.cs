using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.Commands.Categorias;
using Hypesoft.Application.DTOs.Categorias;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Hypesoft.Application.Handlers.Categorias
{
    public class CreateCategoriaHandler : IRequestHandler<CreateCategoriaCommand, CategoriaResponseDto>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CreateCategoriaHandler(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<CategoriaResponseDto> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = new Categoria
            {
                Nome = request.Categoria.Nome
            };

            var criado = await _categoriaRepository.CreateAsync(categoria);

            return _mapper.Map<CategoriaResponseDto>(criado);
        }
    }
}