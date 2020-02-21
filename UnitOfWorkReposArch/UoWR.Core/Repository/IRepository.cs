using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UoWR.Arch.Core.Repository
{
    /// <summary>

    /// Generic repository class that defineds operation related to CRUD.

    /// </summary>

    /// <typeparam name=”T”></typeparam>

    public interface IRepository<T> : IDisposable
    {

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name=”id”>The identifier.</param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name=”index”>The index.</param>
        /// <param name=”count”>The count.</param>
        /// <returns></returns>
        IQueryable<T> GetAll(int index, int count);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name=”entity”>The entity.</param>
        void Update(T entity);


        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name=”entity”>The entity.</param>
        void Insert(T entity);


        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name=”entity”>The entity.</param>
        void Delete(T entity);

    }
}
