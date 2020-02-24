using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace UoWR.Arch.Core.Repository
{
    /// <summary>
    ///  Generic repository class that defineds operation related to CRUD.
    /// </summary>
    /// <typeparam name=”T”></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        public DbContext context;
        public DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the Repository<T> class.
        /// </summary>
        /// <param name=”context”>The context.</param>
        public Repository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name=”id”>The identifier.</param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name=”index”></param>
        /// <param name=”count”></param>
        /// <returns></returns>
        public IQueryable<T> GetAll(int index, int count)
        {
            return dbSet.Skip(index).Take(count);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name=”entity”>The entity.</param>
        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name=”entity”>The entity.</param>
        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name=”entity”>The entity.</param>
        public void Delete(T entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        private bool disposed = false;

        /// <summary>
        /// Releases unmanaged and – optionally – managed resources.
        /// </summary>
        /// <param name=”disposing”><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
