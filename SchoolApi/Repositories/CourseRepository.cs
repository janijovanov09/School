using Microsoft.EntityFrameworkCore;
using SchoolApi.Database;
using SchoolApi.Entities;

namespace SchoolApi.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> GetById(long id);
    Task<Course> Create(Course course);
    Task<Course> Update(Course course);
    Task<bool> Delete(long id);

}

public class CourseRepository : ICourseRepository
{
    private readonly SchoolDbContext _dbContext;

    public CourseRepository(SchoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Course> Create(Course course)
    {
        await _dbContext.Courses.AddAsync(course);
        await _dbContext.SaveChangesAsync();
        return await _dbContext.Courses.Include(c => c.CourseSchedules).SingleOrDefaultAsync(x => x.Id == course.Id);
    }

    public async Task<bool> Delete(long id)
    {
        var course = await _dbContext.Courses.SingleOrDefaultAsync(x => x.Id == id);
        if (course != null)
        {
            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<Course>> GetAllAsync() => await _dbContext.Courses.Include(c => c.CourseSchedules).ToListAsync();

    public async Task<Course> GetById(long id) => await _dbContext.Courses.Include(c => c.CourseSchedules).SingleOrDefaultAsync(x => x.Id == id);

    public async Task<Course> Update(Course course)
    {
        _dbContext.Courses.Update(course);
        await _dbContext.SaveChangesAsync();
        return await _dbContext.Courses.Include(c => c.CourseSchedules).SingleOrDefaultAsync(x => x.Id == course.Id);
    }
}
