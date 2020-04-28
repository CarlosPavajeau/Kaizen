using Kaizen.Core.Domain;
using Kaizen.Domain.Data;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kaizen.Domain.Repositories
{
    public abstract class RepositoryBase<T, TKey> : IRepositoryBase<T, TKey> where T : class
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        public RepositoryBase(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public virtual IQueryable<T> GetAll()
        {
            return ApplicationDbContext.Set<T>().AsNoTracking();
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return ApplicationDbContext.Set<T>().Where(expression);
        }

        public virtual void Insert(T entity)
        {
            ApplicationDbContext.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            ApplicationDbContext.Set<T>().Update(entity);
        }

        public virtual T FindById(TKey id)
        {
            return ApplicationDbContext.Set<T>().Find(id);
        }

        public virtual async Task<T> FindByIdAsync(TKey id)
        {
            return await ApplicationDbContext.Set<T>().FindAsync(id);
        }
    }
}
