using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using WebAPI_MAM.Entities;
using static Azure.Core.HttpHeader;

namespace WebAPI_MAM
{
    public class ApplicationDbContext : IdentityDbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //DISABLE CASCADE DELETING


            modelBuilder.Entity<Patients>()
                .HasOne<MedicInfo>(s => s.medicInfo)
                .WithOne(x=> x.patient)
                .HasForeignKey<MedicInfo>(C=>C.patientId)
                .OnDelete(DeleteBehavior.Restrict);


            
        }
    }
}
