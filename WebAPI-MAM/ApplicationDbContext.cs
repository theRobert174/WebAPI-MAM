using Microsoft.EntityFrameworkCore;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //public DbSet<Entidad> Entidad { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<MedicInfo> MedicInfo { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
    }
}
