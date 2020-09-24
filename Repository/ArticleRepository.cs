using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Database;
using Model;

namespace Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IDatabase _database;

        public ArticleRepository(IDatabase database)
        {
            _database = database;
        }


        public async Task<IArticle> CreateAsync(IArticle model)
        {
            using var connection = _database.Connection;
            connection.Open();
            var id = await connection.InsertAsync(model);
            connection.Close();
            model.Id = id;
            return model;
        }

        public async Task<IEnumerable<IArticle>> ReadAsync()
        {
            using var connection = _database.Connection;
            connection.Open();
            var rows = await connection.GetAllAsync<Article>();
            connection.Close();
            return rows;
        }

        public async Task<IEnumerable<IArticle>> ReadAsync(int id)
        {
            using var connection = _database.Connection;
            connection.Open();
            var row = await connection.GetAsync<Article>(id);
            var rows = new List<IArticle> {row};
            connection.Close();
            return rows;
        }

        //public Task<IEnumerable<IArticle>> ReadLikeAsync(string pattern)
        //{
        //    // pursue if performance issues
        //    // TODO: fetch dynamic table lookup
        //    const string sql = "SELECT * FROM WHERE ";
        //}

        public async Task<bool> UpdateAsync(IArticle model)
        {
            using var connection = _database.Connection;
            connection.Open();
            var isUpdated = await connection.UpdateAsync(model);
            connection.Close();
            return isUpdated;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _database.Connection;
            connection.Open();
            var trx = connection.BeginTransaction();
            var model = await connection.GetAsync<Article>(id);
            var isDeleted = await connection.DeleteAsync(model);
            trx.Commit();
            connection.Close();
            return isDeleted;
        }
    }
}