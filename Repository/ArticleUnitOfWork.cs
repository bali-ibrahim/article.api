using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Interface;

namespace Repository
{
    public class ArticleUnitOfWork : ISingleRepository<Article>
    {
        private readonly ArticleContext _context;

        public ArticleUnitOfWork(ArticleContext context)
        {
            _context = context;
        }

        public async Task<Article> ReadAsync(int id)
        {
            var item = (await (from m in _context.Meta
                    join c in _context.Context on m.Id equals c.MetaId
                    where m.Id == id
                    select new Article
                    {
                        Id = m.Id,
                        Title = m.Title,
                        AuthorFullName = m.AuthorFullName,
                        LastEditedTimestamp = m.LastEditedTimestamp,
                        Body = c.Body
                    }).ToListAsync()).Single()
                ;
            return item;
        }

        public async Task<Article> CreateAsync(Article model)
        {
            var insertedMetaId = (await _context.Meta.AddAsync(model.Meta)).Property(t => t.Id).CurrentValue;
            await _context.Context.AddAsync(model.Context);
            var stateEntryCount = await _context.SaveChangesAsync(CancellationToken.None);
            var created = await ReadAsync(insertedMetaId);
            return created;
        }

        public async Task<bool> UpdateAsync(Article model)
        {
            var metaEntry = _context.Meta.Update(model.Meta);
            var contextEntry = _context.Context.Update(model.Context);
            var stateEntryCount = await _context.SaveChangesAsync();
            return (stateEntryCount > 0);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // TODO: make on delete cascade work
            var context = _context.Context.Single(t => t.MetaId == id);
            _context.Context.Attach(context);
            _context.Context.Remove(context);
            var meta = new Meta {Id = id};
            _context.Meta.Attach(meta);
            _context.Meta.Remove(meta);
            var stateEntryCount = await _context.SaveChangesAsync();
            return (stateEntryCount > 0);
        }
    }
}