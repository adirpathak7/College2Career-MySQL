using College2Career.Models;
using Microsoft.EntityFrameworkCore;

namespace College2Career.Data
{
    public class C2CDBContext : DbContext
    {
        public C2CDBContext(DbContextOptions<C2CDBContext> options) : base(options)
        {

        }

        public C2CDBContext()
        {
            
        }
        public DbSet<Roles> Roles { get; set; } 
        public DbSet<Users> Users { get; set; }
        public DbSet<Colleges> Colleges { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Placements> Placements { get; set; }
        public DbSet<Vacancies> Vacancies { get; set; }
        public DbSet<Applications> Applications { get; set; }
        public DbSet<Interviews> Interviews { get; set; }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<Feedbacks> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly define the one-to-one relationship between Student and Users
            modelBuilder.Entity<Students>()
               .HasOne(s => s.Users)   // Students has one User
               .WithOne(u => u.Students)  // Users has one Student
               .HasForeignKey<Students>(s => s.usersId)  // The foreign key is in Students
               .OnDelete(DeleteBehavior.Cascade); // Define delete behavior


            // Explicitly define the one-to-one relationship between Applications and Offers
            modelBuilder.Entity<Applications>()
                .HasOne(a => a.Offers)    // Applications has one Offer
                .WithOne(o => o.Applications)  // Offers has one Application
                .HasForeignKey<Offers>(o => o.applicationId)  // Offers table holds the FK
                .OnDelete(DeleteBehavior.Cascade); // Define delete behavior

            // Feedbacks table has three foreign keys
            modelBuilder.Entity<Feedbacks>()
            .HasOne(f => f.Students)
            .WithMany(s => s.Feedbacks)
            .HasForeignKey(f => f.studentId)
            .OnDelete(DeleteBehavior.SetNull); // Set null on delete

            modelBuilder.Entity<Feedbacks>()
                .HasOne(f => f.Companies)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(f => f.companyId)
                .OnDelete(DeleteBehavior.SetNull); // Set null on delete

            modelBuilder.Entity<Feedbacks>()
                .HasOne(f => f.Colleges)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(f => f.collegeId)
                .OnDelete(DeleteBehavior.SetNull); // Set null on delete

            base.OnModelCreating(modelBuilder);
        }
    }
}
