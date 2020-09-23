using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public interface IArticleRepository
    {
        Task<IEnumerable<IArticle>> ReadAsync();
        Task<IEnumerable<IArticle>> ReadAsync(int id);
        Task<IArticle> CreateAsync(IArticle model);
        Task<bool> UpdateAsync(IArticle model);
        Task<bool> DeleteAsync(int id);
    }
}