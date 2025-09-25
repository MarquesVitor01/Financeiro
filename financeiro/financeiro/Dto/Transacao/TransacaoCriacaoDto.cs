using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace financeiro.Dto.Transacao
{
    public class TransacaoCriacaoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Valor é obrigatório.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "CategoriaId é obrigatório.")]
        public int CategoriaId { get; set; }

        public string? CategoriaNome { get; set; }

        public string? Observacoes { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;

    }
}