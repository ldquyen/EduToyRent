using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IToyRepository : IRepository<Toy>
    {
        Task<List<Toy>> GetAllToy();
        Task<List<Toy>> GetToysForSale();
        Task<List<Toy>> GetToysForRent();
        Task<Toy> GetToyById(int toyId);
        Task<bool> UpdateToy(Toy toy);
        Task<bool> CheckSameTypeOfToy(List<int> toyIds, bool isRent);
        Task<bool> CheckExistToy(List<int> toyIds);
        Task<decimal> GetMoneyRentByToyId(int toyId,int quantity, DateTime? RentalDate, DateTime? ReturnDate);
        Task<decimal> GetMoneySaleByToyId(int toyId, int quantity);
        Task SubtractQuantity(int toyId, int quantity);
        Task<bool> CheckQuantity(int quantity, int toyId);
    }
}
