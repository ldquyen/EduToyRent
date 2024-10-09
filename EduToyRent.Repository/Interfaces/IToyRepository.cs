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
        Task<int> GetCountAsync();
        Task<IEnumerable<Toy>> GetAllAsync(int pageIndex, int pageSize);
        Task<int> GetCountByName(string keyword);
        Task<IEnumerable<Toy>> SearchByName(string keyword, int pageIndex, int pageSize);
        Task<IEnumerable<Toy>> SortToy(string sortBy, int pageIndex, int pageSize);
        Task<bool> CheckSameTypeOfToy(List<int> toyIds, bool isRent);
    }
}
