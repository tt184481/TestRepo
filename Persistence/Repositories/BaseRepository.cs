using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public AppDbContext _context = null;
        public DbSet<T> dbSet = null;
        public BaseRepository(AppDbContext context)
        {
            this._context = context;
            this.dbSet = this._context.Set <T>();
        }

        public async Task<T> Insert(T model)
        {
            var obj = await dbSet.AddAsync(model); ;
           
            return obj.Entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var models = await dbSet.ToListAsync();

            return models;
        }

        public async Task<T> GetById(int id)
        {
            var model = await dbSet.FindAsync(id);

            return model;
        }

        public void Update(T model)
        {
            var obj = dbSet.Update(model);
        }

        public void Delete(T model)
        {
            if (model != null)
            {
                dbSet.Remove(model);
            }
        }

        public async Task DeleteByID(int Id)
        {
            T existing = await dbSet.FindAsync(Id);

            if (existing != null)
                dbSet.Remove(existing);
        }
    }
}
