using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pnk.Web.Models.Dto;
using Pnk.Web.Models.ViewModel;
using Pnk.Web.Services.IServices;

namespace Pnk.Web.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("list-products")]
        public async Task<IActionResult> ListAllProducts()
        {
            try
            {
                List<ProductViewModel> pList = new();
                var result = await this.productService.GetAllProductsAsync<ResponseDto>();
                if(result.Result != null && result.IsSuccess)
                {
                    var listofProductDTO = JsonConvert.DeserializeObject<List<ProductDto>>(result.Result.ToString());
                    pList = this.mapper.Map<List<ProductViewModel>>(listofProductDTO);
                   
                }
                return View("ProductList",pList);
            }
            catch(Exception e)
            {
                // build error view Model 

                var errorViewModel = new ErrorViewModel
                {
                    DisplayMessage = "Some error occurred. Could not retrieve products data;",
                    StatusCode = 500

                };
                return View(errorViewModel);

            }
        }

    }
}
