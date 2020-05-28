using Domain.Models;
using Domain.Repositories;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class SaleProductsRepository : BaseRepository<SaleProducts>, ISaleProductRepository
    {
        public SaleProductsRepository(AppDbContext context) : base(context) { }
    }
}
