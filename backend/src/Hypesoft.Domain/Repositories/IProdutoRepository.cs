
using System.Collections.Generic;
using System.Threading.Tasks;
using Hypesoft.Domain.Entities;

namespace Hypesoft.Domain.Repositories
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> GetAllAsync();
        Task<Produto?> GetByIdAsync(string id);
        Task<Produto> CreateAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task DeleteAsync(string id);
    }
}