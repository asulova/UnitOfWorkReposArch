using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UoWR.Arch.Persistence
{
    public class EfRepositoryDBContext : DbContext
    {

        /// <summary>
        /// Initializes the <see cref=”EfRepositoryDBContext”/> class.
        /// </summary>
        static EfRepositoryDBContext()
        {
            Database.SetInitializer<EfRepositoryDBContext>(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref=”EfRepositoryDBContext”/> class.
        /// </summary>
        public EfRepositoryDBContext()
            : base("EfRepositoryDBContext")
        {
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name=”modelBuilder”>The builder that defines the model for the context being created.</param>
        /// <remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.Load("UoWR.Arch.Domain").GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null 
                    && type.BaseType.IsGenericType 
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
