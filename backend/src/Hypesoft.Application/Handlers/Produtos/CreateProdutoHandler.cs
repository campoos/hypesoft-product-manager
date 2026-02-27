using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.Commands.Produtos;
using Hypesoft.Application.DTOs.Produtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Data.Common;
using AutoMapper;

namespace Hypesoft.Application.Handlers.Produtos
{
    public class CreateProdutoHandler : IRequestHandler<CreateProdutoCommand, ProdutoResponseDto>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CreateProdutoHandler(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<ProdutoResponseDto> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
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

            return _mapper.Map<ProdutoResponseDto>(criado);
        }
    }
}