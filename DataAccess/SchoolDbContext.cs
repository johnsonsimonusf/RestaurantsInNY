using Linq_practice_studnet_6.Models;
using Microsoft.EntityFrameworkCore;

namespace Linq_practice_studnet_6.DataAccess
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        /*public DbSet<College> Colleges { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }*/

        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Nutrition> Nutrition { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>()
                .HasOne(b => b.Nutrition)
                .WithOne(i => i.Menu)
                .HasForeignKey<Nutrition>(b => b.Menu_ForeignKey);
        }
    }
}