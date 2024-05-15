using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Infrastructure.Interface.Common
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> Entities
        {
            get;
        }

        Task<List<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);

        IEnumerable<T> GetAll(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        T GetOne(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = "");

        Task<T> GetOneAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null);

        T GetFirst(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "");

        Task<T> GetFirstAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);

        bool GetExists(Expression<Func<T, bool>> filter = null);

        Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);

        #region Crud Operations
        void AddRange(IEnumerable<T> entity);

        void AddRangeAsync(IEnumerable<T> entity);

        void Add(T entity);        

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entity);

        void DetachEntry(T entity);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

        void Save();

        Task<long> SaveAsync();
        #endregion

        T Find(Expression<Func<T, bool>> match);

        Task<T> FindAsync(Expression<Func<T, bool>> match);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> match);

        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match);

        Task<IEnumerable<T>> FindAllIEnumerableAsync(Expression<Func<T, bool>> match);

        IQueryable<T> GetQueryable(
                    Expression<Func<T, bool>> filter = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                    string includeProperties = null,
                    int? skip = null,
                    int? take = null);
        
        void Dispose();
    }
}
