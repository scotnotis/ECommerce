using ECommerca.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerca.Api.Products.Controllers
{
    //Public facing API
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        public readonly IProductsProvider productsProvider;  //is a ProductsProvider type

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }       

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productsProvider.GetProductsAsync();

            if(result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productsProvider.GetProductAsync(id);

            if(result.IsSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound();
        }
    }
}
