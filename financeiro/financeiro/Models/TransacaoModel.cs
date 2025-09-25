using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace financeiro.Models
{
    public class TransacaoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }


        public int CategoriaId { get; set; }

        public CategoriaModel? Categoria { get; set; }

        public string? Observacoes { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;

    }
}
