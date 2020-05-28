using Domain.Models;
using Domain.Services.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ISaleProductService
    {
        Task<IEnumerable<SaleProducts>> ListAsync();
        Task<SaleProducts> GetById(int id);
        Task<SaleProductsResponse> InsertAsync(SaleProducts product);
        Task<SaleProductsResponse> UpdateAsync(int id, SaleProducts product);
        Task<SaleProductsResponse> DeleteAsync(SaleProducts product);
        Task<SaleProductsResponse> DeleteByIDAsync(int id);
        Task DeleteBySaleID(int SaleID);
    }
}
