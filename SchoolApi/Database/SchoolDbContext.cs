using Microsoft.EntityFrameworkCore;
using SchoolApi.Entities;

namespace SchoolApi.Database;

public class SchoolDbContext : DbContext
{
    private ReadJsonData _dbInitializer;
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options, ReadJsonData dbInitializer)
        : base(options)
    {
        _dbInitializer = dbInitializer;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseParticipant>()
          .HasKey(p => new { p.CourseId, p.ParticipantId });
        modelBuilder.Entity<CourseSchedule>()
          .HasKey(p => new { p.CourseId, p.Date });
        Seed(modelBuilder);
    }

    public void Seed(ModelBuilder modelBuilder)
    {        
        var courses = _dbInitializer.ReadJson();
        modelBuilder.Entity<Course>().HasData(courses);
        var courseSchedules = new List<CourseSchedule>();

        foreach (var course in courses)
        {
            foreach (var date in course.Dates)
            {
                courseSchedules.Add(new CourseSchedule { CourseId = course.Id, Date = date });
            }
        }

        modelBuilder.Entity<CourseSchedule>().HasData(courseSchedules);
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<CourseParticipant> CourseParticipants { get; set; }
    public DbSet<CourseSchedule> CourseSchedules { get; set; }
}