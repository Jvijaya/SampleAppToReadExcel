using Sample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Infrastructure.Persistence.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.EmployeeNo).HasMaxLength(50).IsRequired();
            builder.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(100).IsRequired();
            builder.HasIndex(p => p.FirstName);
            builder.HasIndex(p => p.LastName);
        }
    }
}
