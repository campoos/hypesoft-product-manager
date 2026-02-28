
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.Exceptions;
using Hypesoft.Application.DTOs.Produtos;
using Hypesoft.Application.DTOs.Categorias;
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
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public UpdateProdutoHandler(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<ProdutoResponseDto> Handle(UpdateProdutoCommand request, CancellationToken cancellationToken)
        {
            var produtoEncontrado = await _produtoRepository.GetByIdAsync(request.Id);

            if (produtoEncontrado == null)
                throw new NotFoundException("Produto não encontrado.");

            var categoria = await _categoriaRepository.GetByIdAsync(request.Produto.CategoriaId);

            if(categoria == null)
                throw new NotFoundException("Categoria não encontrada.");

            produtoEncontrado.Nome = request.Produto.Nome;
            produtoEncontrado.Descricao = request.Produto.Descricao;
            produtoEncontrado.Preco = request.Produto.Preco;
            produtoEncontrado.CategoriaId = request.Produto.CategoriaId;
            produtoEncontrado.QuantidadeEmEstoque = request.Produto.QuantidadeEmEstoque;

            var atualizado = await _produtoRepository.UpdateAsync(produtoEncontrado);

            var dto = _mapper.Map<ProdutoResponseDto>(atualizado);

            dto.Categoria = new CategoriaResumoDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome  
            };

            return dto; 
        }
    }
}
