namespace Hypesoft.Application.DTOs
{
    public class ProdutoDto
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public decimal Preco { get; set; }
        public string CategoriaId { get; set; } = null!;
        public int QuantidadeEmEstoque { get; set; }
    }
}