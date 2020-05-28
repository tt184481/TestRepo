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
using Resources.RequestResources;
using Resources.SaveResources;

namespace BogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ISaleProductService _saleProductService;
        private readonly IMapper _mapper;

        public SalesController(ISaleService saleService, ISaleProductService saleProductService, IMapper mapper)
        {
            _saleService = saleService;
            _saleProductService = saleProductService; 
            _mapper = mapper;
        }

        [HttpGet]
        [Route(nameof(Get))]
        public async Task<IEnumerable<SaleResource>> Get()
        {
            var sales = await _saleService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);

            return resources;
        }

        [HttpGet]
        [Route(nameof(GetByID))]
        public async Task<SaleResource> GetByID(int id)
        {
            var sale = await _saleService.GetById(id);
            var resource = _mapper.Map<Sale, SaleResource>(sale);

            return resource;
        }

        [HttpPost]
        [Route(nameof(Post))]
        public async Task<IActionResult> Post([FromBody] SaveSaleResource resource)
        {
            var sale = _mapper.Map < SaveSaleResource, Sale>(resource);
            var result = await _saleService.InsertAsync(sale);
            var saleResource = _mapper.Map<Sale, SaleResource>(result.Resource);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(saleResource);
        }

        [HttpPut]
        [Route(nameof(Put))]
        public async Task<IActionResult> Put(int id, [FromBody] SaveSaleResource resource)
        {
            var sale = _mapper.Map<SaveSaleResource, Sale>(resource);
            var result = await _saleService.UpdateAsync(id, sale);
            var saleResource = _mapper.Map<Sale, SaleResource>(result.Resource);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(saleResource);
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _saleService.DeleteByIDAsync(id);

            if (!result.Success)
            {
                return BadRequest();
            }
            await _saleProductService.DeleteBySaleID(id);

            return Ok(result.Resource);
        }

        [HttpGet]
        [Route(nameof(GetSaleConsultantsAsync))]
        public async Task<IEnumerable<SaleConsultantResource>> GetSaleConsultantsAsync([FromQuery] GetSaleConsultantsResource request)
        {
            var resource = await _saleService.GetSaleConsultantsAsync(request.StartDate, request.EndDate);

            return resource;
        }

        [HttpGet]
        [Route(nameof(GetSalesPrices))]
        public async Task<IEnumerable<SaleProductPriceResource>> GetSalesPrices([FromQuery] GetSalesPricesResource request)
        {
            var resource = await _saleService.GetSalesPrices(request.StartDate, request.EndDate, request.MinPrice, request.MaxPrice);

            return resource;
        }
    }
}