using Microsoft.EntityFrameworkCore;
using System;

namespace Sample.Infrastructure.Persistence
{
    public partial class SampleDbContext : DbContext
    { 
        public SampleDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
