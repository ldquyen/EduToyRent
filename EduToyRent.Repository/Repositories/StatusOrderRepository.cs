using EduToyRent.DAL.Context;
using EduToyRent.DataAccess.Entities;
using EduToyRent.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Repositories
{
    public class StatusOrderRepository : Repository<StatusOrder> , IStatusOrderRepository
    {
        private readonly EduToyRentDbContext _context;
        public StatusOrderRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
