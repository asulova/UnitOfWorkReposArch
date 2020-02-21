using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UoWR.Arch.Persistence
{
    public class GenericRepositoryDBContext : DbContext

    {

        /// <summary>
        /// Initializes the <see cref=”GenericRepositoryDBContext”/> class.
        /// </summary>

        static GenericRepositoryDBContext()
        {
            System.Data.Entity.Database.SetInitializer<GenericRepositoryDBContext>(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref=”GenericRepositoryDBContext”/> class.
        /// </summary>
        public GenericRepositoryDBContext()
            : base("GenericRepositoryDBContext")
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
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.

        /// </remarks>

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.Load("Domain").GetTypes()
                                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
