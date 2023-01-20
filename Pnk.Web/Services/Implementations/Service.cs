using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.Extensions.Options;
using Pnk.Web.Models.Configuration;
using Pnk.Web.Models.Dto;
using Pnk.Web.Services.IServices;

namespace Pnk.Web.Services.Implementations
{
    public class Service : BaseService, IProductService
    {
        private readonly IOptions<ServiceURLConfiguration> options;
        
        public Service(IHttpClientFactory httpClientFactory,
            IOptions<ServiceURLConfiguration> options
            ) : base(httpClientFactory)
        {
            this.options = options;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto)
        {
            var requestAPI = new APIRequest
            {
                CallType = ServiceConfiguration.CallType.POST,
                Payload = productDto,
                RequestURL = this.options.Value.ProductAPIBaseUrl + "/" + this.options.Value.CreateProduct

            };

            return await this.SendRequestAysnc<T>(requestAPI);
        }

        public Task<T> DeleteProduct<T>(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            // build api request 
            var apiRequestModel = new APIRequest
            {
                CallType = ServiceConfiguration.CallType.GET,
                RequestURL = this.options.Value.ProductAPIBaseUrl + "/" + this.options.Value.ProductList,
            };

           return await this.SendRequestAysnc<T>(apiRequestModel);

        }

        public async Task<T> GetProductByIDAsync<T>(int id)
        {
            var requestAPI = new APIRequest
            {
                CallType = ServiceConfiguration.CallType.GET,
                
                RequestURL = this.options.Value.ProductAPIBaseUrl + "/"+this.options.Value.GetProductById+ "/"+ id,

            };

            return await this.SendRequestAysnc<T>(requestAPI);
        }

        public async Task<T> UpdateProductASync<T>(ProductDto productDto)
        {
            var requestAPI = new APIRequest
            {
                CallType = ServiceConfiguration.CallType.PUT,
                Payload = productDto,
                RequestURL = this.options.Value.ProductAPIBaseUrl + "/" + this.options.Value.UpdateProduct

            };

            return await this.SendRequestAysnc<T>(requestAPI);
        }
    }
}
