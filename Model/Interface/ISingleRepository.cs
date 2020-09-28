using System.Threading.Tasks;
using Model.Data.Interface;

namespace Model.Interface
{
    public interface ISingleRepository<T> where T : IEntity
    {
        Task<T> ReadAsync(int id);
        Task<T> CreateAsync(T model);
        Task<bool> UpdateAsync(T model);
        Task<bool> DeleteAsync(int id);
    }
}