using Finance.Api.Interfaces;
using financeiro.Dto.Categoria;
using financeiro.Dto.Transacao;
using financeiro.Models;

using Microsoft.AspNetCore.Mvc;

namespace financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoInterface _transacaoService;
        public TransacaoController(ITransacaoInterface transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpGet("ListarTransacoes")]
        public async Task<ActionResult<ResponseModel<List<TransacaoModel>>>> ListarTransacoes()
        {
            var transacoes = await _transacaoService.ListarTransacao();
            return Ok(transacoes);
        }

        [HttpGet("BuscarTransacaoPorId/{idTransacao}")]
        public async Task<ActionResult<ResponseModel<TransacaoModel>>> BuscarTransacaoPorId(int idTransacao)
        {
            var transacao = await _transacaoService.BuscarTransacaoPorId(idTransacao);
            if (transacao.Dados == null)
                return NotFound(transacao);

            return Ok(transacao);
        }

        [HttpGet("BuscarTransacaoPorIdCategoria/{idCategoria}")]
        public async Task<ActionResult<ResponseModel<List<TransacaoModel>>>> BuscarTransacaoPorIdCategoria(int idCategoria)
        {
            var transacao = await _transacaoService.BuscarTransacaoPorIdCategoria(idCategoria);
            if (transacao.Dados == null || !transacao.Dados.Any())
                return NotFound(transacao);

            return Ok(transacao);
        }

        [HttpPost("AdicionarTransacao")]
        public async Task<ActionResult<ResponseModel<List<TransacaoModel>>>> AdicionarTransacao(TransacaoCriacaoDto transacaoCriacaoDto)
        {
            var transacao = await _transacaoService.AdicionarTransacao(transacaoCriacaoDto);
            return Ok(transacao);
        }
        [HttpPut("EditarTransacao")]
        public async Task<ActionResult<ResponseModel<List<TransacaoModel>>>> EditarTransacao(TransacaoEdicaoDto transacaoEdicaoDto)
        {
            var transacao = await _transacaoService.EditarTransacao(transacaoEdicaoDto);
            if (transacao.Dados == null)
            {
                return NotFound(transacao);
            }
            return Ok(transacao);
        }

        [HttpDelete("ExcluirTransacao/{idTransacao}")]
        public async Task<ActionResult<ResponseModel<List<TransacaoModel>>>> ExcluirTransacao(int idTransacao)
        {
            var transacao = await _transacaoService.ExcluirTransacao(idTransacao);
            if (transacao.Dados == null)
                return NotFound(transacao);

            return Ok(transacao);
        }
    }

}
