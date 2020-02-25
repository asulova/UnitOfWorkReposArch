using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace UoWR.Arch.Persistence
{
    public class EfRepositoryDBContext : DbContext
    {        
        public EfRepositoryDBContext(DbContextOptions<EfRepositoryDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("UoWR.Arch.Domain"));
            var typesToRegister = Assembly.Load("UoWR.Arch.Domain")
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
