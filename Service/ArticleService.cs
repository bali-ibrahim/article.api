using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Repository;

namespace Service
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<IArticle>> GetAsync(int? id)
        {
            var rows = id == null ? await _repository.ReadAsync() : await _repository.ReadAsync((int) id);
            return rows;
        }

        public async Task<IEnumerable<IArticle>> SearchAsync(string pattern)
        {
            var rows = await _repository.ReadAsync();

            var filteredRows = rows.Where(r => r.Contains(pattern));

            return filteredRows;
        }

        public async Task<IEnumerable<IArticle>> DetailedSearchAsync(IArticle model)
        {
            var rows = await _repository.ReadAsync();


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
            var row = await _repository.CreateAsync(model);
            Debug.Assert(row.Id != null, "row.Id != null");
            return (int) row.Id;
        }

        public async Task<bool> UpdateAsync(IArticle model)
        {
            var isSuccessful = await _repository.UpdateAsync(model);
            return isSuccessful;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var isSuccessful = await _repository.DeleteAsync(id);
            return isSuccessful;
        }
    }
}