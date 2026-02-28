using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

using Hypesoft.Domain.Entities;

namespace Hypesoft.Infrastructure.Data
{
    public class MongoSeeder
    {
        private readonly IMongoCollection<Produto> _produtos;
        private readonly IMongoCollection<Categoria> _categorias;

        public MongoSeeder(MongoContext context)
        {
            _produtos = context.Database.GetCollection<Produto>("Produtos");
            _categorias = context.Database.GetCollection<Categoria>("Categorias");
        }

        public async Task SeedAsync()
        {
            var categoriasExistem = await _categorias.Find(_ => true).AnyAsync();
            if (categoriasExistem) return;

            // =========================
            // 📦 CATEGORIAS
            // =========================
            var categoria1 = new Categoria();
            categoria1.Nome = "Eletrônicos";

            var categoria2 = new Categoria();
            categoria2.Nome = "Roupas";

            var categoria3 = new Categoria();
            categoria3.Nome = "Acessórios";

            var categorias = new List<Categoria>
            {
                categoria1,
                categoria2,
                categoria3
            };

            await _categorias.InsertManyAsync(categorias);

            // =========================
            // 🛒 PRODUTOS
            // =========================
            var produtos = new List<Produto>();

            // ELETRÔNICOS
            produtos.Add(CriarProduto("Notebook Gamer", "Notebook potente para jogos", 4500, categoria1.Id, 10));
            produtos.Add(CriarProduto("Smartphone Android", "Celular moderno com ótima câmera", 2500, categoria1.Id, 25));
            produtos.Add(CriarProduto("Monitor 144Hz", "Monitor ideal para jogos competitivos", 1200, categoria1.Id, 15));
            produtos.Add(CriarProduto("Teclado Mecânico", "Teclado RGB para gamers", 350, categoria1.Id, 40));
            produtos.Add(CriarProduto("Mouse Gamer", "Mouse com alta precisão", 200, categoria1.Id, 50));

            // ROUPAS
            produtos.Add(CriarProduto("Camiseta Básica", "Camiseta confortável de algodão", 50, categoria2.Id, 100));
            produtos.Add(CriarProduto("Calça Jeans", "Calça jeans tradicional", 120, categoria2.Id, 60));
            produtos.Add(CriarProduto("Jaqueta Corta Vento", "Jaqueta leve para frio", 180, categoria2.Id, 30));
            produtos.Add(CriarProduto("Moletom", "Moletom quente e confortável", 150, categoria2.Id, 45));
            produtos.Add(CriarProduto("Shorts Esportivo", "Ideal para atividades físicas", 70, categoria2.Id, 80));

            // ACESSÓRIOS
            produtos.Add(CriarProduto("Relógio Digital", "Relógio esportivo resistente", 200, categoria3.Id, 25));
            produtos.Add(CriarProduto("Óculos de Sol", "Proteção UV e estilo", 90, categoria3.Id, 70));
            produtos.Add(CriarProduto("Boné", "Boné ajustável", 40, categoria3.Id, 90));
            produtos.Add(CriarProduto("Mochila", "Mochila resistente para uso diário", 130, categoria3.Id, 35));
            produtos.Add(CriarProduto("Carteira", "Carteira compacta", 60, categoria3.Id, 55));

            await _produtos.InsertManyAsync(produtos);
        }

        // 🔥 Método auxiliar (clean code)
        private Produto CriarProduto(string nome, string descricao, decimal preco, string categoriaId, int estoque)
        {
            var produto = new Produto();
            produto.Nome = nome;
            produto.Descricao = descricao;
            produto.Preco = preco;
            produto.CategoriaId = categoriaId;
            produto.QuantidadeEmEstoque = estoque;

            return produto;
        }
    }
}