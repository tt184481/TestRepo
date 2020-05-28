using Domain.Models;
using Domain.Services.Response;
using Resources.GetResources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IConsultantService
    {
        Task<IEnumerable<Consultant>> ListAsync();
        Task<Consultant> GetById(int id);
        Task<ConsultantResponse> InsertAsync(Consultant consultant);
        Task<ConsultantResponse> UpdateAsync(int id, Consultant consultant);
        Task<ConsultantResponse> DeleteAsync(Consultant consultant);
        Task<ConsultantResponse> DeleteByIDAsync(int id);
        Task<IEnumerable<ConsultantSaleSumsResource>> GetConsultantSaleSums(DateTime? StartDate, DateTime? EndDate);
        Task<IEnumerable<ConsultantPopularProductResource>> GetMostPopularProducts(DateTime? StartDate, DateTime? EndDate);
        Task<IEnumerable<ConsultantsByProductResource>> GetConsultantsByProducts
                                                (DateTime StartDate, DateTime EndDate, int MinQuantity, long? ProductCode);
    }
}
