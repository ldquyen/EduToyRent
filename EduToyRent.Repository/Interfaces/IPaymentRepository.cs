﻿using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task UpdatePayment(Payment payment);
        Task<List<Payment>> GetPaymentForStaff(int status);
    }
}
