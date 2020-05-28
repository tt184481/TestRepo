using AutoMapper;
using Domain.Models;
using Resources.GetResources;
using Resources.RequestResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BogProject.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Consultant, ConsultantResource>();

            CreateMap<Product, ProductResource>();

            CreateMap<Sale, SaleResource>();

            CreateMap<SaleProducts, SaleProductsResource>();

            CreateMap<IEnumerable<Product>, IEnumerable<ProductResource>>();

            CreateMap<IEnumerable<Consultant>, IEnumerable<ConsultantResource>>();

            CreateMap<IEnumerable<Sale>, IEnumerable<SaleResource>>();

            CreateMap<IEnumerable<SaleProducts>, IEnumerable<SaleProductsResource>>();
        }
    }
}
