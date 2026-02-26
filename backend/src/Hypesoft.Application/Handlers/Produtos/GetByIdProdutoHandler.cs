
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.DTOs.Produtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hypesoft.Application.Queries.Produtos;
using System.Collections.Generic;

namespace Hypesoft.Application.Handlers.Produtos
{
   public class GetByIdProdutoHandler : IRequestHandler<GetByIdProdutosQuery, ProdutoResponseDto>
    {
        private readonly IProdutoRepository _produtoRepository;

        public GetByIdProdutoHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<ProdutoResponseDto> Handle(GetByIdProdutosQuery request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.GetByIdAsync(request.Id);

            if (produto == null)
                throw new KeyNotFoundException("Produto não encontrado");

            return new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                CategoriaId = produto.CategoriaId,
                QuantidadeEmEstoque = produto.QuantidadeEmEstoque
            };
        }
    }
}
