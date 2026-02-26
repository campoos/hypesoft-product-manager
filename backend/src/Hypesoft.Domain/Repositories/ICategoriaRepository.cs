using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hypesoft.Domain.Entities;

namespace Hypesoft.Domain.Repositories
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> GetAllAsync();
        Task<Categoria?> GetByIdAsync(string id);
        Task<Categoria> CreateAsync(Categoria categoria);
        Task<Categoria> UpdateAsync(Categoria categoria);
        Task DeleteAsync(string id);
    }
}