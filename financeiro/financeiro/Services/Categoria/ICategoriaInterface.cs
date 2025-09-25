using financeiro.Dto.Categoria;

namespace financeiro.Services.Categoria
{
    public interface ICategoriaInterface
    {
        Task<Models.ResponseModel<List<Models.CategoriaModel>>> ListarCategorias();
        Task<Models.ResponseModel<Models.CategoriaModel>> BuscarCategoriaPorId(int idCategoria);
        Task<Models.ResponseModel<Models.CategoriaModel>> BuscarCategoriaPorIdTransacao(int idTransacao);
        Task<Models.ResponseModel<List<Models.CategoriaModel>>> AdicionarCategoria(CategoriaCriacaoDto categoriaCriacaoDto);
        Task<Models.ResponseModel<List<Models.CategoriaModel>>> EditarCategoria(CategoriaCriacaoDto categoriaEdicaoDto);
        Task<Models.ResponseModel<List<Models.CategoriaModel>>> ExcluirCategoria(int idCategoria);
    }
}
