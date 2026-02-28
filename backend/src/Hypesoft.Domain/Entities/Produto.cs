
using Hypesoft.Domain.Exceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Hypesoft.Domain.Entities
{
    public class Produto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        public Produto()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
        
        private string _nome = string.Empty;
        public string Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                    throw new DomainValidationException("Nome do produto deve ter pelo menos 3 caracteres.");
                
                _nome = value;
            }
        }

        private string _descricao = string.Empty;
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                    throw new DomainValidationException("Descrição no mínimo deve ter pelo menos 3 caracteres.");
                
                _descricao = value;
            }
        }

        private decimal _preco;
        public decimal Preco
        {
            get => _preco;
            set
            {
                if (value < 0)
                    throw new DomainValidationException("Preço do produto não pode ser negativo.");

                _preco = value;
            }
        }
        private string _categoriaId = string.Empty;
        public string CategoriaId
        {
            get => _categoriaId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomainValidationException("Produto deve ter uma categoria.");

                _categoriaId = value;
            }
        }

        private int _quantidadeEmEstoque;
        public int QuantidadeEmEstoque
        {
            get => _quantidadeEmEstoque;
            set
            {
                if (value < 0)
                    throw new DomainValidationException("Quantidade em estoque deve ser zero ou maior.");
            
                _quantidadeEmEstoque = value;
            }
        }
    }
}