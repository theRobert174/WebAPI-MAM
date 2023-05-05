using Microsoft.EntityFrameworkCore;

namespace WebAPI_MAM
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //public DbSet<Entidad> Entidad { get; set; }
    }
}
