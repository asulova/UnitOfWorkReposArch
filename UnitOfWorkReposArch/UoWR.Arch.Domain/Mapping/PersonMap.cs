using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UoWR.Arch.Domain.Mapping
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> personConfiguration)
        {
            personConfiguration.ToTable("Person");

            personConfiguration.HasKey(o => o.Id);
            personConfiguration.Property<string>("FirstName").IsRequired(false);
            personConfiguration.Property<string>("LastName").IsRequired(false);
        }
    }
}
