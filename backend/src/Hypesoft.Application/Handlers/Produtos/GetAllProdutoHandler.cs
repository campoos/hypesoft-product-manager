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

namespace Hypesoft.Application.Handlers.Produtos
{
    public class GetAllProdutoHandler : IRequestHandler<GetAllProdutosQuery, List<ProdutoResponseDto>>
    {
        private readonly IProdutoRepository _produtoRepository;

        public GetAllProdutoHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProdutoResponseDto>> Handle(GetAllProdutosQuery request, CancellationToken cancellationToken)
        {
            var produtos = await _produtoRepository.GetAllAsync();

            return produtos.Select(produto => new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                CategoriaId = produto.CategoriaId,
                QuantidadeEmEstoque = produto.QuantidadeEmEstoque
            }).ToList();
        }
    }
}
