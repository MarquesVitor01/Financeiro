using System.Text.Json.Serialization;

namespace financeiro.Models
{
    public class CategoriaModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Tipo { get; set; }
        public bool Ativo { get; set; }

        [JsonIgnore] // 🔥 evita loop no Swagger
        public ICollection<TransacaoModel> Transacoes { get; set; } = new List<TransacaoModel>();
    }
}