using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace EduToyRent.DAL.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> LoginAsync(string email, string password);
        Task<bool> CheckEmailExistAsync(string email);
        Task<bool> CheckPhoneExistAsync(string phone);
    }
}
