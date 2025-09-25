using financeiro.Dto.Categoria;
using financeiro.Models;
using financeiro.Services.Categoria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaInterface _categoriaService;
        public CategoriaController(ICategoriaInterface categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("ListarCategorias")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> ListarCategorias()
        {
            var categorias = await _categoriaService.ListarCategorias();
            return Ok(categorias);
        }

        [HttpGet("BuscarCategoriaPorId/{idCategoria}")]
        public async Task<ActionResult<ResponseModel<CategoriaModel>>> BuscarCategoriaPorId(int idCategoria)
        {
            var categoria = await _categoriaService.BuscarCategoriaPorId(idCategoria);
            if (categoria.Dados == null)
            {
                return NotFound(categoria);
            }
            return Ok(categoria);
        }

        [HttpGet("BuscarCategoriaPorIdTransacao/{idTransacao}")]
        public async Task<ActionResult<ResponseModel<CategoriaModel>>> BuscarCategoriaPorIdTransacao(int idTransacao)
        {
            var categoria = await _categoriaService.BuscarCategoriaPorIdTransacao(idTransacao);
            if (categoria.Dados == null)
            {
                return NotFound(categoria);
            }
            return Ok(categoria);
        }

        [HttpPost("AdicionarCategoria")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> AdicionarCategoria(CategoriaCriacaoDto categoriaCriacaoDto)
        {
            var categorias = await _categoriaService.AdicionarCategoria(categoriaCriacaoDto);
            return Ok(categorias);
        }
        [HttpPut("EditarCategoria")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> EditarCategoria(CategoriaCriacaoDto categoriaEdicaoDto)
        {
            var categorias = await _categoriaService.EditarCategoria(categoriaEdicaoDto);
            if (categorias.Dados == null)
            {
                return NotFound(categorias);
            }
            return Ok(categorias);
        }
        [HttpDelete("ExcluirCategoria/{idCategoria}")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> ExcluirCategoria(int idCategoria)
        {
            var categorias = await _categoriaService.ExcluirCategoria(idCategoria);
            if (categorias.Dados == null)
            {
                return NotFound(categorias);
            }
            return Ok(categorias);
        }
    }
}
