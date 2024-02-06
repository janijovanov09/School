using Microsoft.EntityFrameworkCore;
using SchoolApi.Database;
using SchoolApi.Entities;

namespace SchoolApi.Repositories;

public interface ICourseScheduleRepository
{
    Task<IEnumerable<CourseSchedule>> GetAllAsync();
    Task<IEnumerable<CourseSchedule>> GetById(long id);
    Task<IEnumerable<CourseSchedule>> Create(CourseSchedule courseSchedule);
    Task<IEnumerable<CourseSchedule>> Update(CourseSchedule courseSchedule);
    Task<bool> Delete(long id);

}

public class CourseScheduleRepository : ICourseScheduleRepository
{
    private readonly SchoolDbContext _dbContext;

    public CourseScheduleRepository(SchoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CourseSchedule>> Create(CourseSchedule courseSchedule)
    {
        await _dbContext.CourseSchedules.AddAsync(courseSchedule);
        await _dbContext.SaveChangesAsync();
        return await _dbContext.CourseSchedules.Where(x => x.CourseId == courseSchedule.CourseId).ToListAsync();
    }

    public async Task<bool> Delete(long id)
    {
        var schedules = await _dbContext.CourseSchedules.Where(x => x.CourseId == id).ToListAsync();
        if (schedules?.Count > 0)
        {
            _dbContext.CourseSchedules.RemoveRange(schedules);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<CourseSchedule>> GetAllAsync() => await _dbContext.CourseSchedules.ToListAsync();

    public async Task<IEnumerable<CourseSchedule>> GetById(long id) => await _dbContext.CourseSchedules.Where(x => x.CourseId == id).ToListAsync();

    public async Task<IEnumerable<CourseSchedule>> Update(CourseSchedule courseSchedule)
    {
        _dbContext.CourseSchedules.Update(courseSchedule);
        await _dbContext.SaveChangesAsync();
        return await _dbContext.CourseSchedules.Where(x => x.CourseId == courseSchedule.CourseId).ToListAsync();
    }
}
