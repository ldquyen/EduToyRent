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
        Task<Toy> GetToyById(int toyId);
        Task<bool> UpdateToy(Toy toy);
        Task<int> GetRentCount();
        Task<int> GetSaleCount();
        Task<IEnumerable<Toy>> ViewToysForRent(int pageIndex, int pageSize);
        Task<IEnumerable<Toy>> ViewToysForSale(int pageIndex, int pageSize);
        Task<int> GetCountByToyName(string keyword, bool isRental);
        Task<IEnumerable<Toy>> SearchToysByName(string keyword, bool isRental, int pageIndex, int pageSize);
        Task<IEnumerable<Toy>> SortToysForRent(string sortBy, int pageIndex, int pageSize);
        Task<IEnumerable<Toy>> SortToysForSale(string sortBy, int pageIndex, int pageSize);
        Task<bool> CheckSameTypeOfToy(List<int> toyIds, bool isRent);
        Task<bool> CheckExistToy(List<int> toyIds);
        Task<decimal> GetMoneyRentByToyId(int toyId,int quantity, DateTime? RentalDate, DateTime? ReturnDate);
        Task<decimal> GetMoneySaleByToyId(int toyId, int quantity);
    }
}
