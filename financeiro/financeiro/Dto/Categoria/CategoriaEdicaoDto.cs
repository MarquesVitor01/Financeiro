namespace financeiro.Dto.Categoria
{
    public class CategoriaEdicaoDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Tipo { get; set; }
        public bool Ativo { get; set; }
    }
}
