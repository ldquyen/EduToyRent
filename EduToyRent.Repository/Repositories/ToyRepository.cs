﻿using EduToyRent.DAL.Context;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Repositories
{
    public class ToyRepository: Repository<Toy>, IToyRepository
    {
        private readonly EduToyRentDbContext _context;
        public ToyRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }
    }
}