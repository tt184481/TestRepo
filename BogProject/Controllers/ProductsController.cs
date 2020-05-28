using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resources.GetResources;
using Resources.SaveResources;

namespace BogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(nameof(Get))]
        public async Task<IEnumerable<ProductResource>> Get()
        {
            var products = await _productService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

            return resources;
        }

        [HttpGet]
        [Route(nameof(GetByID))]
        public async Task<ProductResource> GetByID(int id)
        {
            var product = await _productService.GetById(id);
            var resource = _mapper.Map<Product, ProductResource>(product);

            return resource;
        }

        [HttpPost]
        [Route(nameof(Post))]
        public async Task<IActionResult> Post([FromBody] SaveProductResource resource)
        {
            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _productService.InsertAsync(product);
            var productResource = _mapper.Map<Product, ProductResource>(result.Resource);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(productResource);
        }

        [HttpPut]
        [Route(nameof(Put))]
        public async Task<IActionResult> Put(int id, [FromBody] SaveProductResource resource)
        {
            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _productService.UpdateAsync(id, product);
            var productResource = _mapper.Map<Product, ProductResource>(result.Resource);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(productResource);
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteByIDAsync(id);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result.Resource);
        }
    }
}