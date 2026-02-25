namespace Hypesoft.Application.DTOs.Produtos
{
    public class ProdutoRequestDto
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public decimal Preco { get; set; }
        public string CategoriaId { get; set; } = null!;
        public int QuantidadeEmEstoque { get; set; }
    }
}