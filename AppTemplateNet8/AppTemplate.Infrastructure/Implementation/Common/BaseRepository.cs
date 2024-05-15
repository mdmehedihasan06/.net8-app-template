using AppTemplate.Domain.DBContexts;
using AppTemplate.Infrastructure.Interface.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;

namespace AppTemplate.Infrastructure.Implementation.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Declare the context
        /// </summary>
        private readonly AppDbContext context;

        /// <summary>
        /// Declare the set
        /// </summary>
        private DbSet<T> set;

        /// <summary>
        /// set disposed value
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The Context</param>
        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets Entities
        /// </summary>
        public virtual IQueryable<T> Entities
        {
            get { return Set; }
        }

        /// <summary>
        /// Gets the set value
        /// </summary>
        protected DbSet<T> Set
        {
            get { return set ??= context.Set<T>(); }
        }

        #region GetMethods
        /// <summary>
        /// Get all IEntity list
        /// </summary>
        /// <returns>IEntity list</returns>
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await Set?.ToListAsync();
        }

        /// <summary>
        /// Get all IEntity with cancellation Token
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>IEntity list</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Set.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Get all with filters
        /// </summary>
        /// <param name="orderBy">The Order by</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <param name="skip">A skip count</param>
        /// <param name="take">A take count</param>
        /// <returns>IEntity list</returns>
        public virtual IEnumerable<T> GetAll(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(null, orderBy, includeProperties, skip, take);
        }

        /// <summary>
        /// Async IEntity list with filter
        /// </summary>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <param name="skip">A skip count</param>
        /// <param name="take">A take count</param>
        /// <returns>IEntity list</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(null, orderBy, includeProperties, skip, take).ToListAsync();
        }

        /// <summary>
        /// Get Entity with predict
        /// </summary>
        /// <param name="filter">The filter value</param>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <param name="skip">A skip count</param>
        /// <param name="take">A take count</param>
        /// <returns>IEntity list</returns>
        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take);
        }

        /// <summary>
        /// Get Async Entity with predict
        /// </summary>
        /// <param name="filter">The filter value</param>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <param name="skip">A skip count</param>
        /// <param name="take">A take count</param>
        /// <returns>The Entity</returns>
        public virtual async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        /// <summary>
        /// Get First Or default entity object
        /// </summary>
        /// <param name="filter">Filter Function</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <returns>Single Entity</returns>
        public virtual T GetOne(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = "")
        {
            return GetQueryable(filter, null, includeProperties).FirstOrDefault();
        }

        /// <summary>
        /// Get Async First Or default entity object
        /// </summary>
        /// <param name="filter">Filter Function</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <returns>Single Entity</returns>
        public virtual async Task<T> GetOneAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null)
        {
            return await GetQueryable(filter, null, includeProperties).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get First entity object
        /// </summary>
        /// <param name="filter">Filter Function</param>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <returns>First Entity</returns>
        public virtual T GetFirst(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "")
        {
            return GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();
        }

        /// <summary>
        /// Get Async First entity object
        /// </summary>
        /// <param name="filter">Filter Function</param>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <returns>First Entity</returns>
        public virtual async Task<T> GetFirstAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get counts
        /// </summary>
        /// <param name="filter">A filter</param>
        /// <returns>Counts value</returns>
        public virtual long GetCount(Func<T, long> filter)
        {
            if (!Set.Any())
            {
                return 0;
            }
            else
            {
                return Set.Max(filter);
            }
        }

        /// <summary>
        /// Check exists based on prediction
        /// </summary>
        /// <param name="filter">filter Function</param>
        /// <returns>boolean value</returns>
        public virtual bool GetExists(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        /// <summary>
        /// Check Async exists based on prediction
        /// </summary>
        /// <param name="filter">filter Function</param>
        /// <returns>boolean value</returns>
        public virtual Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).AnyAsync();
        }
        #endregion

        #region Crud Operations
        /// <summary>
        /// Add Object in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void Add(T entity)
        {
            Set.Add(entity);
        }

        /// <summary>
        /// Add Object range in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void AddRange(IEnumerable<T> entity)
        {
            Set.AddRange(entity);
        }

        public virtual void AddRangeAsync(IEnumerable<T> entity)
        {
            Set.AddRangeAsync(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entity)
        {
            Set.UpdateRange(entity);
        }

        /// <summary>
        /// Update Object in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void Update(T entity)
        {
            Set.Update(entity);
        }

        /// <summary>
        /// Detach Entry in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void DetachEntry(T entity)
        {
            context.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// Remove object in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void Remove(T entity)
        {
            Set.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entity)
        {
            Set.RemoveRange(entity);
        }

        /// <summary>
        /// Save the transection
        /// </summary>
        public virtual void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Save Async the transection
        /// </summary>
        /// <returns>Response long value</returns>
        public async virtual Task<long> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        #endregion

        #region Find
        /// <summary>
        /// Get entity based on expression
        /// </summary>
        /// <param name="match">The Expression</param>
        /// <returns>The Entity</returns>
        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return Set.FirstOrDefault(match);
        }

        /// <summary>
        /// Get async entity based on expression
        /// </summary>
        /// <param name="match">The Expression</param>
        /// <returns>The Entity</returns>
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await Set.FirstOrDefaultAsync(match);
        }

        /// <summary>
        /// Get entity list based on expression
        /// </summary>
        /// <param name="match">The Expression</param>
        /// <returns>The Entity list</returns>
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match)
        {
            return Set.Where(match);
        }

        /// <summary>
        /// Get async entity list based on expression
        /// </summary>
        /// <param name="match">The Expression</param>
        /// <returns>The Entity list</returns>
        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await Set.Where(match).ToListAsync();
        }

        /// <summary>
        /// Get async entity list based on expression
        /// </summary>
        /// <param name="match">The Expression</param>
        /// <returns>The Entity list</returns>
        public async Task<IEnumerable<T>> FindAllIEnumerableAsync(Expression<Func<T, bool>> match)
        {
            return await Set.Where(match).ToListAsync();
        }

        #endregion

        /// <summary>
        /// Get Query able Entities with predict
        /// </summary>
        /// <param name="filter">The filter value</param>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <param name="skip">A skip count</param>
        /// <param name="take">A take count</param>
        /// <returns>The Entity</returns>
        public virtual IQueryable<T> GetQueryable(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties ??= string.Empty;
            IQueryable<T> query = Set;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        /// <summary>
        /// Dispose transaction
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposed based on variable value
        /// </summary>
        /// <param name="disposing">Disposing value</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposed = true;
            }
        }
    }
}
