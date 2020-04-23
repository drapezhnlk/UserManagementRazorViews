using System.Collections.Generic;
using UserManagementRazorViews.Entities;

namespace UserManagementRazorViews.Interfaces
{
    public interface ITitleRepository : IRepository<Title>
    {
        public void Create(IEnumerable<Title> titles);
        
        IEnumerable<Title> Get(IEnumerable<string> titlesNames);
    }
}