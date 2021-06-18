using Sample.Application.Interfaces;
using Sample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sample.Infrastructure.Persistence
{
    public partial class SampleDbContext: ISampleDbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
