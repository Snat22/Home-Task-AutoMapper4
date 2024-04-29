using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {


    }


    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Mentor> Mentors { get; set; } = null!;
    public DbSet<StudentGroup> StudentGroups { get; set; } = null!;
    public virtual DbSet<MentorGroup> MentorGroups { get; set; } = null!;
    public DbSet<ProgressBook> ProgressBook { get; set; }
    public DbSet<TimeTable> TimeTable { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Course>()
            .HasMany(x => x.Groups)
            .WithOne(x => x.Course)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(builder);
    }
}
