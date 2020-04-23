using System.Collections.Generic;
using System.Linq;
using UserManagementRazorViews.DatabaseContext;
using UserManagementRazorViews.Entities;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Repositories
{
    public class TitleRepository : BaseRepository<Title>, ITitleRepository
    {
        public TitleRepository(AppDbContext dbContext) : base(dbContext) {}

        public void Create(IEnumerable<Title> titles)
        {
            DbContext.Titles.AddRange(titles);
            DbContext.SaveChanges();
        }

        public IEnumerable<Title> Get(IEnumerable<string> titlesNames)
        {
            return DbContext.Titles.Where(t => titlesNames.Contains(t.Name));
        }
    }
}