using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kaizen.Core.Domain
{
    public interface IRepositoryBase<T, in TKey> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        void Insert(T entity);

        Task<T> FindByIdAsync(TKey id);
        void Update(T entity);
        void Delete(T entity);
    }
}
