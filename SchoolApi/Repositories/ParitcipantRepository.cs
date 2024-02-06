using Microsoft.EntityFrameworkCore;
using SchoolApi.Database;
using SchoolApi.Entities;

namespace SchoolApi.Repositories;

public interface IParticipantRepository
{
    Task<IEnumerable<Participant>> GetAllAsync();
    Task<Participant> GetById(Guid id);
    Task<Participant> Create(Participant participant);
    Task<Participant> Update(Participant articipant);
    Task<bool> Delete(Guid id);

}

public class ParticipantRepository : IParticipantRepository
{
    private readonly SchoolDbContext _dbContext;

    public ParticipantRepository(SchoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Participant> Create(Participant participant)
    {
        await _dbContext.Participants.AddAsync(participant);
        await _dbContext.SaveChangesAsync();
        return await _dbContext.Participants.SingleOrDefaultAsync(x => x.Id == participant.Id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var participant = await _dbContext.Participants.SingleOrDefaultAsync(x => x.Id == id);
        if (participant != null)
        {
            _dbContext.Participants.Remove(participant);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<Participant>> GetAllAsync() => await _dbContext.Participants.ToListAsync();

    public async Task<Participant> GetById(Guid id) => await _dbContext.Participants.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<Participant> Update(Participant participant)
    {
        _dbContext.Participants.Update(participant);
        await _dbContext.SaveChangesAsync();
        return await _dbContext.Participants.SingleOrDefaultAsync(x => x.Id == participant.Id);
    }
}
