using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public interface IArticleService
    {
        Task<IEnumerable<IArticle>> GetAsync(int? id);
        Task<IEnumerable<IArticle>> SearchAsync(IArticle model);
        Task<int> AddAsync(IArticle model);
        Task<bool> UpdateAsync(IArticle model);
        Task<bool> DeleteAsync(int id);
    }
}