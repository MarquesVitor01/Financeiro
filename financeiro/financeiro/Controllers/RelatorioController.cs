using Finance.Api.Interfaces;
using financeiro.Dto.Relatorio;
using financeiro.Models;
using financeiro.Services.Relatorio;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioInterface _relatorioService;

        public RelatorioController(IRelatorioInterface relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("resumo")]
        public async Task<ActionResult<ResponseModel<ResumoRelatorioDto>>> Resumo([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            var resultado = await _relatorioService.Resumo(dataInicio, dataFim);
            return Ok(resultado);
        }

        [HttpGet("por-categoria")]
        public async Task<ActionResult<ResponseModel<List<RelatorioCategoriaDto>>>> PorCategoria([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            var resultado = await _relatorioService.PorCategoria(dataInicio, dataFim);
            return Ok(resultado);
        }
    }
}
