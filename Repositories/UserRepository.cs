using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagementRazorViews.DatabaseContext;
using UserManagementRazorViews.Entities;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext) {}

        public override User Get(int id)
        {
            return DbContext.Users
                .Include(u => u.Company)
                .Include(u => u.UsersTitles)
                .ThenInclude(u => u.Title)
                .FirstOrDefault(u => u.Id == id);
        }

        public override IEnumerable<User> GetAll()
        {
            return DbContext.Users
                .Include(u => u.Company)
                .Include(u => u.UsersTitles)
                .ThenInclude(u => u.Title);
        }

        public IEnumerable<User> GetByCompanyId(int companyId)
        {
            return DbContext.Users.Where(u => u.Company.Id == companyId);
        }
    }
}