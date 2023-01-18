using Pnk.Web.Models.Dto;

namespace Pnk.Web.Services.IServices
{
    public interface IProductService
    {
        Task<T> GetAllProductsAsync<T>();
        Task<T> GetProductByIDAsync<T>(int id);
        Task<T> CreateProductAsync<T>(ProductDto productDto);
        Task<T> UpdateProductASync<T>(ProductDto productDto);

        Task<T> DeleteProduct<T>(int id);
    }
}
