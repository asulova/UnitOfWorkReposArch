using UoWR.Arch.Core.Repository;

namespace UoWR.Arch.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Repositories this instance.
        /// </summary>
        /// <typeparam name=”T”></typeparam>
        /// <returns></returns>
        IRepository<T> Repository<T>() where T : class;

        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();

    }
}
