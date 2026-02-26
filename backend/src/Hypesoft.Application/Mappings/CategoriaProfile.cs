using AutoMapper;
using Hypesoft.Domain.Entities;
using Hypesoft.Application.DTOs.Categorias;

namespace Hypesoft.Application.Mappings
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categoria, CategoriaResponseDto>();
        }
    }
}