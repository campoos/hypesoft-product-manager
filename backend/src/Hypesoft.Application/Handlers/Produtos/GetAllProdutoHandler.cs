using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.DTOs.Produtos;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Hypesoft.Application.Queries.Produtos;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Hypesoft.Application.Handlers.Produtos
{
    public class GetAllProdutoHandler : IRequestHandler<GetAllProdutosQuery, List<ProdutoResponseDto>>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public GetAllProdutoHandler(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<List<ProdutoResponseDto>> Handle(GetAllProdutosQuery request, CancellationToken cancellationToken)
        {
            var produtos = await _produtoRepository.GetAllAsync();

            return _mapper.Map<List<ProdutoResponseDto>>(produtos);
        }
    }
}
