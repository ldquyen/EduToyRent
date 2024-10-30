using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IDepositOrderRepository
    {
        Task CreateDepositOrder(Order order, string bankcode, string bankname);
    }
}
