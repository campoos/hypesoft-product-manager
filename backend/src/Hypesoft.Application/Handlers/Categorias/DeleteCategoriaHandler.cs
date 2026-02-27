using Hypesoft.Domain.Repositories;
using Hypesoft.Application.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hypesoft.Application.Commands.Categorias;

namespace Hypesoft.Application.Handlers.Categorias
{
    public class DeleteCategoriaHandler : IRequestHandler<DeleteCategoriaCommand>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public DeleteCategoriaHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Unit> Handle(DeleteCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(request.Id);

            if (categoria == null)
                throw new NotFoundException("Categoria não encontrada");

            await _categoriaRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}