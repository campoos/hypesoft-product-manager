using AutoMapper;
using Hypesoft.Domain.Entities;
using Hypesoft.Application.DTOs.Produtos;

namespace Hypesoft.Application.Mappings
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoResponseDto>();
        }
    }
}