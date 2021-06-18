using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sample.Infrastructure.Persistence
{
    public class SampleDbContextDesignFactory : IDesignTimeDbContextFactory<SampleDbContext>
    {
        public SampleDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SampleDbContext>()
                  .UseSqlServer("Server=localhost;Initial Catalog=SampleDB;Integrated Security=true");

            return new SampleDbContext(optionsBuilder.Options);
        }
    }
}
