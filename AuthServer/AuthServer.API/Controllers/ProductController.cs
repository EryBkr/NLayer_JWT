using AuthServer.Core.Dtos;
using AuthServer.Core.Entities;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : CustomBaseController
    {
        private readonly IGenericService<Product,ProductDto> _productService;

        public ProductController(IGenericService<Product, ProductDto> productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var products =await _productService.GetAllAsync();
            return ActionResultInstance(products);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDto model)
        {
           var product=await _productService.AddAsync(model);
            return ActionResultInstance(product);
        }

        [HttpPut]
        public IActionResult UpdateProduct(ProductDto model)
        {
            var product =  _productService.Update(model);
            return ActionResultInstance(product);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(ProductDto model)
        {
            var product = _productService.Remove(model);
            return ActionResultInstance(product);
        }
    }
}
