using Azure.Core;
using EduToyRent.DAL.Context;
using EduToyRent.DAL.Entities;
using EduToyRent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Repositories
{
    public class RequestFormRepository : Repository<RequestForm>, IRequestFormRepository
    {
        private readonly EduToyRentDbContext _context;
        public RequestFormRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
