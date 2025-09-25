namespace financeiro.Dto.Relatorio
{
    public class ResumoRelatorioDto
    {
        public decimal SaldoTotal { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
    }

    public class RelatorioCategoriaDto
    {
        public string Categoria { get; set; } = string.Empty;
        public decimal Total { get; set; }
    }
}
