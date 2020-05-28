using Domain.Models;
using Domain.Services.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<Product> GetById(int id);
        Task<ProductResponse> InsertAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
        Task<ProductResponse> DeleteAsync(Product product);
        Task<ProductResponse> DeleteByIDAsync(int id);
    }
}
