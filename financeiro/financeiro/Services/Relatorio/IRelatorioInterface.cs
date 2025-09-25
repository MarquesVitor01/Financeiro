using financeiro.Dto.Relatorio;
using financeiro.Models;

namespace financeiro.Services.Relatorio
{
    public interface IRelatorioInterface
    {
        Task<ResponseModel<ResumoRelatorioDto>> Resumo(DateTime dataInicio, DateTime dataFim);
        Task<ResponseModel<List<RelatorioCategoriaDto>>> PorCategoria(DateTime dataInicio, DateTime dataFim);
    }
}
