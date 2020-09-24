using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public interface IArticleRepository
    {
        Task<IArticle> CreateAsync(IArticle model);
        Task<IEnumerable<IArticle>> ReadAsync();

        Task<IEnumerable<IArticle>> ReadAsync(int id);

        //Task<IEnumerable<IArticle>> ReadLikeAsync(string pattern);
        Task<bool> UpdateAsync(IArticle model);
        Task<bool> DeleteAsync(int id);
    }
}