using Hypesoft.Application.DTOs.Produtos;
using MediatR;
using System.Collections.Generic;

namespace Hypesoft.Application.Queries.Produtos
{
    public class GetAllProdutosQuery : IRequest<List<ProdutoResponseDto>>
    {

    }
}