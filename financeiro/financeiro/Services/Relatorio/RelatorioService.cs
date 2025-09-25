using Finance.Api.Interfaces;
using financeiro.Data;
using financeiro.Dto.Relatorio;
using financeiro.Models;
using Microsoft.EntityFrameworkCore;

namespace financeiro.Services.Relatorio
{
    public class RelatorioService : IRelatorioInterface
    {
        private readonly AppDbContext _context;

        public RelatorioService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<ResumoRelatorioDto>> Resumo(DateTime dataInicio, DateTime dataFim)
        {
            var resposta = new ResponseModel<ResumoRelatorioDto>();

            try
            {
                var transacoes = await _context.Transacao
                    .Include(t => t.Categoria)
                    .Where(t => t.DataCriacao >= dataInicio && t.DataCriacao <= dataFim)
                    .ToListAsync();

                var receitas = transacoes
                    .Where(t => t.Categoria != null && t.Categoria.Tipo == "Receita")
                    .Sum(t => t.Valor);

                var despesas = transacoes
                    .Where(t => t.Categoria != null && t.Categoria.Tipo == "Despesa")
                    .Sum(t => t.Valor);

                resposta.Dados = new ResumoRelatorioDto
                {
                    SaldoTotal = receitas - despesas,
                    TotalReceitas = receitas,
                    TotalDespesas = despesas
                };

                resposta.Mensagem = "Resumo gerado com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }


        public async Task<ResponseModel<List<RelatorioCategoriaDto>>> PorCategoria(DateTime dataInicio, DateTime dataFim)
        {
            var resposta = new ResponseModel<List<RelatorioCategoriaDto>>();

            try
            {
                resposta.Dados = await _context.Transacao
                    .Include(t => t.Categoria)
                    .Where(t => t.DataCriacao >= dataInicio && t.DataCriacao <= dataFim)
                    .GroupBy(t => t.Categoria!.Nome)
                    .Select(g => new RelatorioCategoriaDto
                    {
                        Categoria = g.Key,
                        Total = g.Sum(t => t.Valor)
                    })
                    .ToListAsync();

                resposta.Mensagem = "Relatório por categoria gerado com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
