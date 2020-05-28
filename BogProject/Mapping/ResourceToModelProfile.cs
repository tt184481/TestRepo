using AutoMapper;
using Domain.Models;
using Resources.SaveResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BogProject.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveConsultantResource, Consultant>();

            CreateMap<SaveProductResource, Product>();

            CreateMap<SaveSaleResource, Sale>();

            CreateMap<SaveSaleProductsResource, SaleProducts>();

            CreateMap<IEnumerable<SaveProductResource>, IEnumerable<Product>>();

            CreateMap<IEnumerable<SaveSaleResource>, IEnumerable<Sale>>();

            CreateMap<IEnumerable<SaveConsultantResource>, IEnumerable<Consultant>>();

            CreateMap<IEnumerable<SaveSaleProductsResource>, IEnumerable<SaleProducts>>();
        }
    }
}
