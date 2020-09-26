using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Interface;

namespace Service
{
    public interface IArticleService
    {
        Task<IEnumerable<IMeta>> GetAsync();
        Task<IEnumerable<IArticle>> GetAsync(int id);
        Task<IEnumerable<IArticle>> SearchAsync(string pattern);
        Task<int> AddAsync(IArticle model);
        Task<bool> UpdateAsync(IArticle model);
        Task<bool> DeleteAsync(int id);
    }
}