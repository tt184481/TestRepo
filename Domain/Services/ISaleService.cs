using Domain.Models;
using Domain.Services.Response;
using Resources.GetResources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> ListAsync();
        Task<Sale> GetById(int id);
        Task<SaleResponse> InsertAsync(Sale sale);
        Task<SaleResponse> UpdateAsync(int id, Sale sale);
        Task<SaleResponse> DeleteAsync(Sale sale);
        Task<SaleResponse> DeleteByIDAsync(int id);
        Task<IEnumerable<SaleConsultantResource>> GetSaleConsultantsAsync(DateTime StartDate, DateTime EndDate);
        Task<IEnumerable<SaleProductPriceResource>> GetSalesPrices(DateTime StartDate, DateTime EndDate, int MinPrice, int MaxPrice);
    }
}
