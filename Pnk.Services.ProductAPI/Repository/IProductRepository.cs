using Pnk.Services.ProductAPI.Models.Dto;

namespace Pnk.Services.ProductAPI.Repository
{
    public  interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int id);

        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);

        Task<bool> DeleteProduct(int id);
    }
}
