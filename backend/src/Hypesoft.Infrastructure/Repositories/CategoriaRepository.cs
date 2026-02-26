using System.Collections.Generic;
using System.Threading.Tasks;
using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Infrastructure.Data;
using MongoDB.Driver;

namespace Hypesoft.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IMongoCollection<Categoria> _collection;

        public CategoriaRepository(MongoContext context)
        {
            _collection = context.Database.GetCollection<Categoria>("Categorias");
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Categoria?> GetByIdAsync(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Categoria> CreateAsync(Categoria categoria)
        {
            await _collection.InsertOneAsync(categoria);
            return categoria;
        }

        public async Task<Categoria> UpdateAsync(Categoria categoria)
        {
            await _collection.ReplaceOneAsync(p => p.Id == categoria.Id, categoria);
            return categoria;
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(p => p.Id == id);
        }
    }
}