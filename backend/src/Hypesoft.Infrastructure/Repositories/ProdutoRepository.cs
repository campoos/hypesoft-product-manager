using System.Collections.Generic;
using System.Threading.Tasks;
using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Infrastructure.Data;
using MongoDB.Driver;

namespace Hypesoft.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IMongoCollection<Produto> _collection;

        public ProdutoRepository(MongoContext context)
        {
            _collection = context.Database.GetCollection<Produto>("Produtos");
        }

        public async Task<List<Produto>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Produto?> GetByIdAsync(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Produto> CreateAsync(Produto produto)
        {
            await _collection.InsertOneAsync(produto);
            return produto;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            await _collection.ReplaceOneAsync(p => p.Id == produto.Id, produto);
            return produto;
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(p => p.Id == id);
        }
    }
}