using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Model.Interface;
using Repository;

namespace Service
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<IArticle> _articleRepository;
        private readonly IRepository<IMeta>_metaRepository;

        public ArticleService(IRepository<IArticle> articleRepository, IRepository<IMeta> metaRepository)
        {
            _articleRepository = articleRepository;
            _metaRepository = metaRepository;
        }

        public async Task<IEnumerable<IMeta>> GetAsync()
        {
            var rows = await _metaRepository.ReadAsync();
            return rows;
        }

        public async Task<IEnumerable<IArticle>> GetAsync(int id)
        {
            var rows = await _articleRepository.ReadAsync(id);
            return rows;
        }

        public async Task<IEnumerable<IArticle>> SearchAsync(string pattern)
        {
            var rows = await _articleRepository.ReadAsync();

            // TODO: move to db level
            var filteredRows = rows.Where(r => r.Contains(pattern));

            return filteredRows;
        }

        public async Task<IEnumerable<IArticle>> DetailedSearchAsync(IArticle model)
        {
            var rows = await _articleRepository.ReadAsync();


            var filteredRows = rows.Where(r => true
                                               //TODO: make it dry
                                               && r.Id.ToString().Contains(model.Id.ToString())
                                               && r.Title.Contains(model.Title)
                                               && r.AuthorFullName.Contains(model.AuthorFullName)
                                               && r.Body.Contains(model.Body)
            );

            return filteredRows;
        }

        public async Task<int> AddAsync(IArticle model)
        {
            var row = await _articleRepository.CreateAsync(model);
            Debug.Assert(row.Id != null, "row.Id != null");
            return (int) row.Id;
        }

        public async Task<bool> UpdateAsync(IArticle model)
        {
            var isSuccessful = await _articleRepository.UpdateAsync(model);
            return isSuccessful;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var isSuccessful = await _articleRepository.DeleteAsync(id);
            return isSuccessful;
        }
    }
}