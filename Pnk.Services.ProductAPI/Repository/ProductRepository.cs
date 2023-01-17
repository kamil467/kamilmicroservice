using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pnk.Services.ProductAPI.DbContexts;
using Pnk.Services.ProductAPI.Models;
using Pnk.Services.ProductAPI.Models.Dto;

namespace Pnk.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            this._context = applicationDbContext;
            this._mapper = mapper;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            // convert productdto to product
            var product = this._mapper.Map<Product>(productDto);
            if(product.ProductId > 0)
            {
                // update operation
                this._context.Update(product);
               
            }
            else
            {
                // create operation.
                this._context.Products.Add(product);
            }

            await this._context.SaveChangesAsync();

            return this._mapper.Map<ProductDto>(product); // ID is available after save changes executed.

        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var product = await this._context.Products
                            .Where(p => p.ProductId==id)
                            .FirstOrDefaultAsync();
                if (product == null)
                    return false;

                this._context.Products.Remove(product);
                await this._context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await this._context.Products
                          .Where(p => p.ProductId == id)
                          .AsNoTracking()
                          .SingleOrDefaultAsync();
            return this._mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await this._context.Products.AsNoTracking().ToListAsync();

            return this._mapper.Map<List<ProductDto>>(products);

        }
    }
}
