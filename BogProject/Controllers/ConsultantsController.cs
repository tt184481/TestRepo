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
    public class ConsultantsController : ControllerBase
    {
        private readonly IConsultantService _consultantService;
        private readonly IMapper _mapper;

        public ConsultantsController(IConsultantService consultantService, IMapper mapper)
        {
            _consultantService = consultantService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(nameof(Get))]
        public async Task<IEnumerable<ConsultantResource>> Get()
        {
            var consultants = await _consultantService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Consultant>, IEnumerable<ConsultantResource>>(consultants);

            return resources;
        }

        [HttpGet]
        [Route(nameof(GetByID))]
        public async Task<ConsultantResource> GetByID(int id)
        {
            var consultant = await _consultantService.GetById(id);
            var resource = _mapper.Map<Consultant, ConsultantResource>(consultant);

            return resource;
        }

        [HttpPost]
        [Route(nameof(Post))]
        public async Task<IActionResult> Post([FromBody] SaveConsultantResource resource)
        {
            var consultant = _mapper.Map<SaveConsultantResource, Consultant>(resource);
            var result = await _consultantService.InsertAsync(consultant);
            var consultantResource = _mapper.Map<Consultant, ConsultantResource>(result.Resource);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(consultantResource);
        }

        [HttpPut]
        [Route(nameof(Put))]
        public async Task<IActionResult> Put(int id, [FromBody] SaveConsultantResource resource)
        {
            var consultant = _mapper.Map<SaveConsultantResource, Consultant>(resource);
            var result = await _consultantService.UpdateAsync(id, consultant);
            var consultantResource = _mapper.Map<Consultant, ConsultantResource>(result.Resource);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(consultantResource);
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _consultantService.DeleteByIDAsync(id);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result.Resource);
        }

        [HttpGet]
        [Route(nameof(GetConsultantSaleSums))]
        public async Task<IActionResult> GetConsultantSaleSums([FromQuery] GetConsultantSaleSumsRequest request)
        {
            var resources = await _consultantService.GetConsultantSaleSums(request.StartDate, request.EndDate); 

            return Ok(resources);
        }

        [HttpGet]
        [Route(nameof(GetMostPopularProducts))]
        public async Task<IActionResult> GetMostPopularProducts([FromQuery] GetMostPopularProductsRequest request)
        {
            var resources = await _consultantService.GetMostPopularProducts(request.StartDate, request.EndDate);

            return Ok(resources);
        }

        [HttpGet]
        [Route(nameof(GetConsultantsByProducts))]
        public async Task<IEnumerable<ConsultantsByProductResource>> GetConsultantsByProducts ([FromQuery] GetConsultantsByProductsRequest request)
                                                
        {
            var resources = await _consultantService.GetConsultantsByProducts
                                                (request.StartDate, request.EndDate, request.MinQuantity, request.ProductCode);

            return resources;
        }
    }
}