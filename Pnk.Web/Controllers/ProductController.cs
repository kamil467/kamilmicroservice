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
        [Route("list-products",Name ="listproducts")]
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
                return View("~/Views/Shared/Error.cshtml", errorViewModel);

            }
        }

        [HttpGet]
        [Route("updateproduct-get/{id}")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            try
            {
                var product = await this.productService.GetProductByIDAsync<ResponseDto>(id);
                if(product.IsSuccess && product.Result != null)
                {
                    var productFromResponse = JsonConvert.DeserializeObject<ProductDto>(product.Result.ToString());
                    var viewModel = this.mapper.Map<ProductViewModel>(productFromResponse);
                    return View("UpdateProduct", viewModel);
                }

                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { DisplayMessage="Unable to perform the operation."});


            }
            catch(Exception exe)
            {
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { DisplayMessage = "Unable to perform the operation."+exe.ToString() });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // tokens by default added in the startup services.
        [Route("updateproduct-post")]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productViewModel)
        {
            if(ModelState.IsValid)
            {
                var productDTO = this.mapper.Map<ProductDto>(productViewModel);
                var result = await this.productService.UpdateProductASync<ResponseDto>(productDTO);
                if(result.IsSuccess)
                {
                    return this.RedirectToAction("list-products");
                }

                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { DisplayMessage = "Unable to perform the operation." });

            }
            return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { DisplayMessage = "Unable to perform the operation." });
        }

        [HttpGet]
        [Route("createproduct-get")]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("createproduct-post")]
        public async Task<IActionResult> CreateProduct(ProductViewModel productViewModel)
        {
            if(this.ModelState.IsValid)
            {
                var productDto = this.mapper.Map<ProductDto>(productViewModel);
                var result = await this.productService.CreateProductAsync<ResponseDto>(productDto);
                if(result.IsSuccess && result.Result != null)
                {
                   return  this.RedirectToAction("list-products");
                }
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { DisplayMessage = "Unable to perform the operation." });
            }
            return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { DisplayMessage = "Validation Failed" });
        }
    }
}
