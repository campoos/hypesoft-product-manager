using Hypesoft.Domain.Repositories;
using Hypesoft.Application.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hypesoft.Application.Commands.Produtos;

namespace Hypesoft.Application.Handlers.Produtos
{
    public class DeleteProdutoHandler : IRequestHandler<DeleteProdutoCommand>
    {
        private readonly IProdutoRepository _produtoRepository;

        public DeleteProdutoHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Unit> Handle(DeleteProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.GetByIdAsync(request.Id);

            if (produto == null)
                throw new NotFoundException("Produto não encontrado");

            await _produtoRepository.DeleteAsync(request.Id);

            return Unit.Value; // MediatR espera Unit quando não retorna nada
        }
    }
}