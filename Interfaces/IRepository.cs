using System.Collections.Generic;
using UserManagementRazorViews.Entities;

namespace UserManagementRazorViews.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T entity);
        T Get(int id);
        void Update(T entity);
        void Delete(int id);
        IEnumerable<T> GetAll();
    }
}