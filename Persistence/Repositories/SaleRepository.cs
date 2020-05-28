using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Resources.GetResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        public SaleRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Sale> GetSalesBetweenConcreteDates(DateTime StartDate, DateTime EndDate)
        {
            var obj = _context.Sales.Where(x => (x.SaleDate >= StartDate && x.SaleDate <= EndDate)).
                Include("Consultant.SaleProducts");

            return obj;
        }
    }
}
