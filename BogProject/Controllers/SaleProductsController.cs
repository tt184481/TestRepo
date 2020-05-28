using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SaleProductsController : ControllerBase
    {
        private readonly ISaleProductService _saleProductsService;
        private readonly IMapper _mapper;

        public SaleProductsController(ISaleProductService saleProductsService, IMapper mapper)
        {
            _saleProductsService = saleProductsService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(nameof(Get))]
        public async Task<IEnumerable<SaleProductsResource>> Get()
        {
            var products = await _saleProductsService.ListAsync();
            var resources = _mapper.Map<IEnumerable<SaleProducts>, IEnumerable<SaleProductsResource>>(products);

            return resources;
        }

        [HttpGet]
        [Route(nameof(GetByID))]
        public async Task<SaleProductsResource> GetByID(int id)
        {
            var product = await _saleProductsService.GetById(id);
            var resource = _mapper.Map<SaleProducts, SaleProductsResource>(product);

            return resource;
        }

        [HttpPost]
        [Route(nameof(Post))]
        public async Task<IActionResult> Post([FromBody] SaveSaleProductsResource resource)
        {
            var product = _mapper.Map<SaveSaleProductsResource, SaleProducts>(resource);
            var result = await _saleProductsService.InsertAsync(product);
            var productResource = _mapper.Map<SaleProducts, SaleProductsResource>(result.Resource);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result.Resource);
        }

        [HttpPut]
        [Route(nameof(Put))]
        public async Task<IActionResult> Put(int id, [FromBody] SaveSaleProductsResource resource)
        {
            var product = _mapper.Map<SaveSaleProductsResource, SaleProducts>(resource);
            var result = await _saleProductsService.UpdateAsync(id, product);
            var productResource = _mapper.Map<SaleProducts, SaleProductsResource>(result.Resource);

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
            var result = await _saleProductsService.DeleteByIDAsync(id);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result.Resource);
        }
    }
}