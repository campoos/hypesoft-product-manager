using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using MongoDB.Driver;

namespace Hypesoft.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IMongoCollection<Produto> _collection;

        public ProdutoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Produto>("Produtos");
        }

        public async Task<Produto> CreateAsync(Produto produto)
        {
            await _collection.InsertOneAsync(produto);
            return produto;
        }
    }
}