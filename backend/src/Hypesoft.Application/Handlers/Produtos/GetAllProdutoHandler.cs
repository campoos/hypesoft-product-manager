
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.DTOs.Produtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hypesoft.Application.Queries.Produtos;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hypesoft.Application.DTOs.Categorias;

namespace Hypesoft.Application.Handlers.Produtos
{
    public class GetAllProdutoHandler : IRequestHandler<GetAllProdutosQuery, List<ProdutoResponseDto>>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public GetAllProdutoHandler(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<List<ProdutoResponseDto>> Handle(GetAllProdutosQuery request, CancellationToken cancellationToken)
        {
            var produtos = await _produtoRepository.GetAllAsync();

            var query = produtos.AsQueryable();

            if (!string.IsNullOrEmpty(request.Nome))
                query = query.Where(p => p.Nome.ToLower().Contains(request.Nome.ToLower()));

            if (!string.IsNullOrEmpty(request.CategoriaId))
                query = query.Where(p => p.CategoriaId == request.CategoriaId);

            if (request.EstoqueMax.HasValue)
                query = query.Where(p => p.QuantidadeEmEstoque <= request.EstoqueMax.Value);

            var filteredProdutos = query.ToList();

            var response = new List<ProdutoResponseDto>();

            foreach (var produto in filteredProdutos)
            {
                var categoria = await _categoriaRepository.GetByIdAsync(produto.CategoriaId);

                var dto = _mapper.Map<ProdutoResponseDto>(produto);

                if(categoria == null)
                    dto.Categoria = null;
                else 
                    dto.Categoria = new CategoriaResumoDto
                    {
                        Id = categoria.Id,
                        Nome = categoria.Nome  
                    };

                response.Add(dto);
            }

            return response;
        }
    }
}
