
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.Exceptions;
using Hypesoft.Application.DTOs.Produtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hypesoft.Application.Commands.Produtos;

namespace Hypesoft.Application.Handlers.Produtos
{
   public class UpdateProdutoHandler : IRequestHandler<UpdateProdutoCommand, ProdutoResponseDto>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public UpdateProdutoHandler(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<ProdutoResponseDto> Handle(UpdateProdutoCommand request, CancellationToken cancellationToken)
        {
            var produtoEncontrado = await _produtoRepository.GetByIdAsync(request.Id);

            if (produtoEncontrado == null)
                throw new NotFoundException("Produto não encontrado");

            produtoEncontrado.Nome = request.Produto.Nome;
            produtoEncontrado.Descricao = request.Produto.Descricao;
            produtoEncontrado.Preco = request.Produto.Preco;
            produtoEncontrado.CategoriaId = request.Produto.CategoriaId;
            produtoEncontrado.QuantidadeEmEstoque = request.Produto.QuantidadeEmEstoque;

            var atualizado = await _produtoRepository.UpdateAsync(produtoEncontrado);

            return _mapper.Map<ProdutoResponseDto>(atualizado);
        }
    }
}
