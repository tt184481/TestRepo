using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Insert(T model);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        void Delete(T model);
        Task DeleteByID(int Id);
        void Update(T model);
    }
}
