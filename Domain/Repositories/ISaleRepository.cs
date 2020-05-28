using Domain.Models;
using Resources.GetResources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        IEnumerable<Sale> GetSalesBetweenConcreteDates(DateTime StartDate, DateTime EndDate);
    }
}
