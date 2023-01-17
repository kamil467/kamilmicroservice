using Microsoft.AspNetCore.Mvc;
using Pnk.Services.ProductAPI.Models.Dto;
using Pnk.Services.ProductAPI.Repository;

namespace Pnk.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductAPI : ControllerBase
    {
        private readonly IProductRepository productRepository;
        protected ResponseDto responseDto;

        public ProductAPI(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            this.responseDto = new ResponseDto();
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<Object> GetProducts()
        {
            try
            {
                var allProducts = await this.productRepository.GetProducts();
                this.responseDto.Result = allProducts;
                this.responseDto.Message = "all available Products are returned";
            }
            catch(Exception e)
            {
                this.responseDto.IsSuccess = false;
                this.responseDto.ErrorMessages = new List<string> { e.ToString() };
            }

            return this.responseDto;

        }

        [HttpGet]
        [Route("getproduct/{id}")]
        public async Task<object> GetProduct(int id)
        {
            try
            {
                var product = await this.productRepository.GetProductById(id);
                this.responseDto.Result = product;
                this.responseDto.Message = "Product returned successfully.";
            }
            catch(Exception e)
            {
                this.responseDto.IsSuccess = false;
                this.responseDto.ErrorMessages = new List<string>
                {
                    e.ToString(),
                };
            }
            return this.responseDto;
        }

        [HttpPost]
        [Route("createproduct")]
        public async Task<object> CreateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                var result = await this.productRepository.CreateUpdateProduct(productDto);
                this.responseDto.Result = result;
                this.responseDto.Message = $"Product with Id {result.ProductId} has been successfully created";
            }
            catch(Exception exe)
            {
                this.responseDto.IsSuccess = false;
                this.responseDto.ErrorMessages = new List<string>
                {
                    exe.ToString(),
                };
            }

            return this.responseDto;
        }

        [HttpPut]
        [Route("updateproduct")]
        public async Task<object> UpdateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                var result = await this.productRepository.CreateUpdateProduct(productDto);
                this.responseDto.Result = result;
                this.responseDto.Message = $"Given product :{result.ProductId} has been updated successfully.";
            }
            catch(Exception exe)
            {
                this.responseDto.IsSuccess = false;
                this.responseDto.ErrorMessages = new List<string>
                {
                    exe.ToString(),
                };
            }
            return this.responseDto;
        }
    }
}
