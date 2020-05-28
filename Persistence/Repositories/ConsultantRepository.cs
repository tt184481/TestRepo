using Domain.Models;
using Domain.Repositories;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class ConsultantRepository : BaseRepository<Consultant>, IConsultantRepository
    {
        public ConsultantRepository(AppDbContext context) : base(context) { }
    }
}
