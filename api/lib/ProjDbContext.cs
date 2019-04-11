using Interview.Models;
using Microsoft.EntityFrameworkCore;

namespace Interview {
    public class ProjDbContext : DbContext {
        public ProjDbContext (DbContextOptions<ProjDbContext> options ): base(options)
        {
            
        }

        public DbSet<Course> Course { get; set; }

        public DbSet<Exam> Exam { get; set; }

        public DbSet<Question> Question { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserCourse> UserCourse { get; set; }

        public DbSet<UserExam> UserExam { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Question>()
            .HasOne(q => q.Exam)
            .WithMany(e => e.Questions)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCourse> ()
                .HasKey (e => new { e.UserId, e.CourseId });
        }
    }
}