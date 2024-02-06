using SchoolApi.Entities;
using SchoolApi.Repositories;

namespace SchoolApi.Endpoints;
public static class ParticpantEndpoints
{
    public static void MapParticpantEndpoints(this WebApplication app)
    {
        app.MapGet("participant", GetAll);
        app.MapGet("participant/{id:guid}", GetById);
        app.MapPost("participant", Create);
        app.MapPut("participant/{id:guid}", Update);
        app.MapDelete("participant/{id:guid}", Delete);
    }

    internal static async Task<IEnumerable<Participant>> GetAll(IParticipantRepository repository)
    {
        return await repository.GetAllAsync();
    }

    internal static async Task<IResult> GetById(IParticipantRepository repository, Guid id)
    {
        var customer = await repository.GetById(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    internal static async Task<IResult> Create(
        IParticipantRepository repository, Participant participant)
    {
        await repository.Create(participant);
        return Results.Created($"/participant/{participant.Id}", participant);
    }

    internal static async Task<IResult> Update(IParticipantRepository repository, Guid id, Participant participant)
    {
        var existing = await repository.GetById(participant.Id);
        if (existing is null)
        {
            return Results.NotFound();
        }

        await repository.Update(participant);
        return Results.Ok(participant);
    }

    internal static async Task<IResult> Delete(IParticipantRepository repository, Guid id)
    {
        var deleted = await repository.Delete(id);
        return deleted ? Results.Ok() : Results.NotFound();
    }
}