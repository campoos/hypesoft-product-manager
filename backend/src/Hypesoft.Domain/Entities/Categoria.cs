
using Hypesoft.Domain.Exceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Hypesoft.Domain.Entities
{
    public class Categoria
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        public Categoria()
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
                    throw new DomainValidationException("Nome da categoria deve ter pelo menos 3 caracteres.");
                
                _nome = value;
            }
        }
    }
}