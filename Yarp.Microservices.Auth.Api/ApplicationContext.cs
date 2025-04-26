using Microsoft.EntityFrameworkCore;

namespace Yarp.Microservices.Auth.Api
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
