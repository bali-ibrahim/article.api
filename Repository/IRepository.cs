using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Interface;

namespace Repository
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> CreateAsync(T model);
        Task<IEnumerable<T>> ReadAsync();
        Task<IEnumerable<T>> ReadAsync(int id);
        Task<bool> UpdateAsync(T model);
        Task<bool> DeleteAsync(int id);
    }
}