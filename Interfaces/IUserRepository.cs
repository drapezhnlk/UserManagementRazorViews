using System.Collections.Generic;
using UserManagementRazorViews.Entities;

namespace UserManagementRazorViews.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetByCompanyId(int companyId);
    }
}