using Microsoft.EntityFrameworkCore;
using SchoolApi.Database;
using SchoolApi.Entities;

namespace SchoolApi.Repositories;

public interface ICourseParticipantRepository
{
    Task<IEnumerable<CourseParticipant>> GetAllAsync();
    Task<IEnumerable<CourseParticipant>> GetById(long id);
    Task<IEnumerable<CourseParticipant>> Create(CourseParticipant courseParticipant);
    Task<IEnumerable<CourseParticipant>> Update(CourseParticipant courseParticipant);
    Task<bool> Delete(long id);

}

public class CourseParticipantRepository : ICourseParticipantRepository
{
    private readonly SchoolDbContext _dbContext;

    public CourseParticipantRepository(SchoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CourseParticipant>> Create(CourseParticipant courseParticipant)
    {
        await _dbContext.CourseParticipants.AddAsync(courseParticipant);
        await _dbContext.SaveChangesAsync();
        return await _dbContext.CourseParticipants.Where(x => x.CourseId == courseParticipant.CourseId).ToListAsync();
    }

    public async Task<bool> Delete(long id)
    {
        var participants = await _dbContext.CourseParticipants.Where(x => x.CourseId == id).ToListAsync();
        if (participants?.Count > 0)
        {
            _dbContext.CourseParticipants.RemoveRange(participants);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<CourseParticipant>> GetAllAsync() => await _dbContext.CourseParticipants.ToListAsync();

    public async Task<IEnumerable<CourseParticipant>> GetById(long id) => await _dbContext.CourseParticipants.Where(x => x.CourseId == id).ToListAsync();

    public async Task<IEnumerable<CourseParticipant>> Update(CourseParticipant courseParticipant)
    {
        _dbContext.CourseParticipants.Update(courseParticipant);
        await _dbContext.SaveChangesAsync();
        return await _dbContext.CourseParticipants.Where(x => x.CourseId == courseParticipant.CourseId).ToListAsync();
    }
}
