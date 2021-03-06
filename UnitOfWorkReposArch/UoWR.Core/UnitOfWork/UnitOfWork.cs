﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UoWR.Arch.Core.Repository;

namespace UoWR.Arch.Core.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {

        private bool disposed = false;
        private Dictionary<string, object> repositories;
        private DbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and – optionally – managed resources.
        /// </summary>
        /// <param name=”disposing”><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        /// <summary>
        /// Repositories this instance.
        /// </summary>
        /// <typeparam name=”T”></typeparam>
        /// <returns></returns>
        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryInstance = new Repository<T>(_dbContext);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)repositories[type];
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
