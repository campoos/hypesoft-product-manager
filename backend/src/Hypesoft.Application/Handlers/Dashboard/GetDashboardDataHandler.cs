using Hypesoft.Application.DTOs.Dashboard;
using Hypesoft.Application.Queries.Dashboard;
using Hypesoft.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hypesoft.Application.Handlers.Dashboard
{
    public class GetDashboardDataHandler : IRequestHandler<GetDashboardDataQuery, DashboardDataDto>
    {
        private readonly IProdutoRepository _produtoRepository;

        public GetDashboardDataHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<DashboardDataDto> Handle(GetDashboardDataQuery request, CancellationToken cancellationToken)
        {
            var produtos = await _produtoRepository.GetAllAsync();

            var totalProdutos = produtos.Count;
            var valorTotalEstoque = produtos.Sum(p => p.Preco * p.QuantidadeEmEstoque);
            var produtosComEstoqueBaixo = produtos.Count(p => p.QuantidadeEmEstoque < 10);

            return new DashboardDataDto
            {
                TotalProdutos = totalProdutos,
                ValorTotalEstoque = valorTotalEstoque,
                ProdutosComEstoqueBaixo = produtosComEstoqueBaixo
            };
        }
    }
}