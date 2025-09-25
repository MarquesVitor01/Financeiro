using financeiro.Dto.Transacao;
using financeiro.Models;

namespace Finance.Api.Interfaces
{
    public interface ITransacaoInterface
    {
        Task<ResponseModel<List<TransacaoCriacaoDto>>> ListarTransacao();
        Task<ResponseModel<TransacaoCriacaoDto>> BuscarTransacaoPorId(int idTransacao);
        Task<ResponseModel<List<TransacaoCriacaoDto>>> BuscarTransacaoPorIdCategoria(int idCategoria);
        Task<ResponseModel<List<TransacaoModel>>> AdicionarTransacao(TransacaoCriacaoDto transacaoCriacaoDto);
        Task<ResponseModel<TransacaoEdicaoDto>> EditarTransacao(TransacaoEdicaoDto transacaoEdicaoDto);

        Task<ResponseModel<List<TransacaoCriacaoDto>>> ExcluirTransacao(int idTransacao);
    }
}
