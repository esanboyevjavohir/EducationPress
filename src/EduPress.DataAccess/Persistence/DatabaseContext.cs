using EduPress.Core.Common;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EduPress.DataAccess.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            //Database.EnsureCreated();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<ContactLocations> ContactLocations { get; set; }
        public DbSet<CourseFaqs> CourseFaqs { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }
        public DbSet<CourseLessons> CourseLessons { get; set; }
        public DbSet<CourseSection> CourseSections { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<LessonProgress> LessonProgresses { get; set; }
        public DbSet<OtpCode> OtpCodes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ReviewReplies> ReviewReplies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedOn = DateTime.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
