
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.Exceptions;
using Hypesoft.Application.DTOs.Produtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hypesoft.Application.Queries.Produtos;
using System.Collections.Generic;
using AutoMapper;

namespace Hypesoft.Application.Handlers.Produtos
{
   public class GetByIdProdutoHandler : IRequestHandler<GetByIdProdutosQuery, ProdutoResponseDto>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public GetByIdProdutoHandler(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<ProdutoResponseDto> Handle(GetByIdProdutosQuery request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.GetByIdAsync(request.Id);

            if (produto == null)
                throw new NotFoundException("Produto não encontrado");

            return _mapper.Map<ProdutoResponseDto>(produto);
        }
    }
}
