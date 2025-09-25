using financeiro.Dto.Categoria;
using financeiro.Models;
using Microsoft.EntityFrameworkCore;

namespace financeiro.Services.Categoria
{
    public class CategoriaService : ICategoriaInterface
    {
        private readonly Data.AppDbContext _context;
        public CategoriaService(Data.AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<CategoriaModel>>> AdicionarCategoria(CategoriaCriacaoDto categoriaCriacaoDto)
        {
            ResponseModel<List<CategoriaModel>> resposta = new ResponseModel<List<CategoriaModel>>();
            try
            {
                var categoria = new CategoriaModel()
                {
                    Nome = categoriaCriacaoDto.Nome,
                    Tipo = categoriaCriacaoDto.Tipo,
                    Ativo = categoriaCriacaoDto.Ativo
                };
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Categoria.ToListAsync();
                resposta.Mensagem = "Categoria adicionada com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<CategoriaModel>> BuscarCategoriaPorId(int idCategoria)
        {
            ResponseModel<CategoriaModel> resposta = new ResponseModel<CategoriaModel>();
            try
            {
                var categoria = await _context.Categoria.FirstOrDefaultAsync(categoriaBanco => categoriaBanco.Id == idCategoria);

                if (categoria == null)
                {
                    resposta.Mensagem = "Categoria não encontrada.";
                    return resposta;
                }

                resposta.Dados = categoria;
                resposta.Mensagem = "Categoria encontrada com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<CategoriaModel>> BuscarCategoriaPorIdTransacao(int idTransacao)
        {
            ResponseModel<CategoriaModel> resposta = new ResponseModel<CategoriaModel>();
            try
            {
                var transacao = await _context.Transacao
                    .Include(c => c.Categoria)
                    .FirstOrDefaultAsync(transacaoBanco => transacaoBanco.Id == idTransacao);

                if (transacao == null || transacao.Categoria == null)
                {
                    resposta.Mensagem = "Transação ou categoria não encontrada.";
                    return resposta;
                }

                resposta.Dados = transacao.Categoria; 
                resposta.Mensagem = "Categoria encontrada com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<CategoriaModel>>> EditarCategoria(CategoriaEdicaoDto categoriaEdicaoDto)
        {
            ResponseModel<List<CategoriaModel>> resposta = new ResponseModel<List<CategoriaModel>>();
            try
            {
                var categoria = await _context.Categoria.FirstOrDefaultAsync(categoriaBanco => categoriaBanco.Id == categoriaEdicaoDto.Id);
                if (categoria == null)
                {
                    resposta.Mensagem = "Categoria não encontrada.";
                    return resposta;
                }
                categoria.Nome = categoriaEdicaoDto.Nome ?? categoria.Nome;
                categoria.Tipo = categoriaEdicaoDto.Tipo ?? categoria.Tipo;
                categoria.Ativo = categoriaEdicaoDto.Ativo;
                _context.Update(categoria);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Categoria.ToListAsync();
                resposta.Mensagem = "Categoria editada com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public Task<ResponseModel<List<CategoriaModel>>> EditarCategoria(CategoriaCriacaoDto categoriaEdicaoDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<CategoriaModel>>> ExcluirCategoria(int idCategoria)
        {
            ResponseModel<List<CategoriaModel>> resposta = new ResponseModel<List<CategoriaModel>>();

            try
            {
                var categoria = await _context.Categoria.FirstOrDefaultAsync(categoriaBanco => categoriaBanco.Id == idCategoria);
                if (categoria == null)
                {
                    resposta.Mensagem = "Categoria não encontrada.";
                    return resposta;
                }   
                _context.Remove(categoria);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Categoria.ToListAsync();
                resposta.Mensagem = "Categoria excluída com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<CategoriaModel>>> ListarCategorias()
        {
            ResponseModel<List<CategoriaModel>> resposta = new ResponseModel<List<CategoriaModel>>();
            try
            {
                var categorias = await _context.Categoria.ToListAsync();
                resposta.Dados = categorias;
                resposta.Mensagem = "Categorias listadas com sucesso.";

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
