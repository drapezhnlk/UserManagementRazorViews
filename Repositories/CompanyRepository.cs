using UserManagementRazorViews.DatabaseContext;
using UserManagementRazorViews.Entities;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {   
        public CompanyRepository(AppDbContext dbContext) : base(dbContext) {}

    }
}