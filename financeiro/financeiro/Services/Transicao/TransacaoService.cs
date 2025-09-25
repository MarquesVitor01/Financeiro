using Finance.Api.Interfaces;
using financeiro.Data;
using financeiro.Dto.Transacao;
using financeiro.Models;
using Microsoft.EntityFrameworkCore;

namespace financeiro.Services.Transacao
{
    public class TransacaoService : ITransacaoInterface
    {
        private readonly AppDbContext _context;

        public TransacaoService(AppDbContext context)
        {
            _context = context;
        }

        // Listar todas as transações
        public async Task<ResponseModel<List<TransacaoCriacaoDto>>> ListarTransacao()
        {
            var resposta = new ResponseModel<List<TransacaoCriacaoDto>>();
            try
            {
                var transacoes = await _context.Transacao
                    .Include(t => t.Categoria)
                    .Select(t => new TransacaoCriacaoDto
                    {
                        Id = t.Id,
                        Descricao = t.Descricao,
                        Valor = t.Valor,
                        CategoriaId = t.CategoriaId,
                        CategoriaNome = t.Categoria!.Nome,
                        Observacoes = t.Observacoes,
                        DataCriacao = t.DataCriacao
                    })
                    .ToListAsync();

                resposta.Dados = transacoes;
                resposta.Mensagem = "Transações listadas com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        // Buscar transação por Id
        public async Task<ResponseModel<TransacaoCriacaoDto>> BuscarTransacaoPorId(int id)
        {
            var resposta = new ResponseModel<TransacaoCriacaoDto>();
            try
            {
                var t = await _context.Transacao
                    .Include(t => t.Categoria)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (t == null)
                {
                    resposta.Mensagem = "Transação não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = new TransacaoCriacaoDto
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    CategoriaId = t.CategoriaId,
                    CategoriaNome = t.Categoria?.Nome,
                    Observacoes = t.Observacoes,
                    DataCriacao = t.DataCriacao
                };

                resposta.Mensagem = "Transação encontrada com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        // Buscar transações por categoria
        public async Task<ResponseModel<List<TransacaoCriacaoDto>>> BuscarTransacaoPorIdCategoria(int idCategoria)
        {
            var resposta = new ResponseModel<List<TransacaoCriacaoDto>>();
            try
            {
                var transacoes = await _context.Transacao
                    .Include(t => t.Categoria)
                    .Where(t => t.CategoriaId == idCategoria)
                    .ToListAsync();

                if (!transacoes.Any())
                {
                    resposta.Mensagem = "Nenhuma transação encontrada para esta categoria.";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = transacoes.Select(t => new TransacaoCriacaoDto
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    CategoriaId = t.CategoriaId,
                    CategoriaNome = t.Categoria?.Nome,
                    Observacoes = t.Observacoes,
                    DataCriacao = t.DataCriacao
                }).ToList();

                resposta.Mensagem = "Transações encontradas com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        // Adicionar transação
        public async Task<ResponseModel<List<TransacaoModel>>> AdicionarTransacao(TransacaoCriacaoDto dto)
        {
            var resposta = new ResponseModel<List<TransacaoModel>>();
            try
            {
                var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == dto.CategoriaId);
                if (categoria == null)
                {
                    resposta.Mensagem = "Categoria não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }

                var transacao = new TransacaoModel
                {
                    Descricao = dto.Descricao,
                    Valor = dto.Valor,
                    CategoriaId = dto.CategoriaId,
                    Observacoes = dto.Observacoes,
                    DataCriacao = DateTime.Now
                };

                _context.Transacao.Add(transacao);
                await _context.SaveChangesAsync(); // Id gerado aqui

                // Retorna todas as transações
                resposta.Dados = await _context.Transacao.Include(t => t.Categoria).ToListAsync();
                resposta.Mensagem = "Transação adicionada com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        // Editar transação
        public async Task<ResponseModel<TransacaoEdicaoDto>> EditarTransacao(TransacaoEdicaoDto transacaoEdicaoDto)
        {
            var resposta = new ResponseModel<TransacaoEdicaoDto>();
            try
            {
                var transacao = await _context.Transacao.FirstOrDefaultAsync(t => t.Id == transacaoEdicaoDto.Id);
                if (transacao == null)
                {
                    resposta.Mensagem = "Transação não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }

                var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == transacaoEdicaoDto.CategoriaId);
                if (categoria == null)
                {
                    resposta.Mensagem = "Categoria não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }

                transacao.Descricao = transacaoEdicaoDto.Descricao;
                transacao.Valor = transacaoEdicaoDto.Valor;
                transacao.CategoriaId = transacaoEdicaoDto.CategoriaId;
                transacao.Observacoes = transacaoEdicaoDto.Observacoes;
                transacao.DataCriacao = transacaoEdicaoDto.DataCriacao;

                await _context.SaveChangesAsync();

                resposta.Dados = transacaoEdicaoDto;
                resposta.Mensagem = "Transação editada com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        // Excluir transação
        public async Task<ResponseModel<List<TransacaoCriacaoDto>>> ExcluirTransacao(int id)
        {
            var resposta = new ResponseModel<List<TransacaoCriacaoDto>>();
            try
            {
                var transacao = await _context.Transacao.FirstOrDefaultAsync(t => t.Id == id);
                if (transacao == null)
                {
                    resposta.Mensagem = "Transação não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }

                _context.Transacao.Remove(transacao);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Transacao
                    .Include(t => t.Categoria)
                    .Select(t => new TransacaoCriacaoDto
                    {
                        Id = t.Id,
                        Descricao = t.Descricao,
                        Valor = t.Valor,
                        CategoriaId = t.CategoriaId,
                        CategoriaNome = t.Categoria!.Nome,
                        Observacoes = t.Observacoes,
                        DataCriacao = t.DataCriacao
                    })
                    .ToListAsync();

                resposta.Mensagem = "Transação excluída com sucesso.";
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
