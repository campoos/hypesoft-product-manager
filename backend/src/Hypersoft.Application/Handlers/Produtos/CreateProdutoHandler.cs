using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.Commands.Produtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hypesoft.Application.Handlers.Produtos
{
    public class CreateProdutoHandler : IRequestHandler<CreateProdutoCommand, ProdutoDto>
    {
        private readonly IProdutoRepository _produtoRepository;

        public CreateProdutoHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<ProdutoDto> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = new Produto
            {
                Nome = request.Produto.Nome,
                Descricao = request.Produto.Descricao,
                Preco = request.Produto.Preco,
                CategoriaId = request.Produto.CategoriaId,
                QuantidadeEmEstoque = request.Produto.QuantidadeEmEstoque
            };

            var criado = await _produtoRepository.CreateAsync(produto);

            return new ProdutoDto
            {
                Nome = criado.Nome,
                Descricao = criado.Descricao,
                Preco = criado.Preco,
                CategoriaId = criado.CategoriaId,
                QuantidadeEmEstoque = criado.QuantidadeEmEstoque
            };
        }
    }
}