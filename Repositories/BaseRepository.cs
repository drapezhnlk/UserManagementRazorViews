using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagementRazorViews.DatabaseContext;
using UserManagementRazorViews.Entities;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private readonly DbSet<T> _dbSet;
        protected readonly AppDbContext DbContext;
        
        protected BaseRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = DbContext.Set<T>();
        }
        
        public virtual void Create(T entity)
        {
            _dbSet.Add(entity);
            DbContext.SaveChanges();
        }

        public virtual T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
            DbContext.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entity = Get(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                DbContext.SaveChanges();    
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }
    }
}