using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.DTOs.Categorias;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Hypesoft.Application.Queries.Categorias;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Hypesoft.Application.Handlers.Categorias
{
    public class GetAllCategoriaHandler : IRequestHandler<GetAllCategoriasQuery, List<CategoriaResponseDto>>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public GetAllCategoriaHandler(ICategoriaRepository produtoRepository, IMapper mapper)
        {
            _categoriaRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoriaResponseDto>> Handle(GetAllCategoriasQuery request, CancellationToken cancellationToken)
        {
            var categorias = await _categoriaRepository.GetAllAsync();

            return _mapper.Map<List<CategoriaResponseDto>>(categorias);
        }
    }
}
