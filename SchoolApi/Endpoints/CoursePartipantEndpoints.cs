using Microsoft.AspNetCore.Mvc;
using SchoolApi.Entities;
using SchoolApi.Repositories;

namespace SchoolApi.Endpoints;

public static class CourseParticipantEndpoints
{
    public static void MapCourseParticpantEndpoints(this WebApplication app)
    {
        app.MapGet("courseparticipant", GetAll);
        app.MapGet("courseparticipant/{id:long}", GetById);
        app.MapPost("courseparticipant", Create);
        app.MapPut("courseparticipant/{id:long}", Update);
        app.MapDelete("courseparticipant/{id:long}", Delete);
    }

    internal static async Task<IEnumerable<CourseParticipant>> GetAll(ICourseParticipantRepository repository)
    {
        return await repository.GetAllAsync();
    }

    internal static async Task<IResult> GetById(ICourseParticipantRepository repository, long id)
    {
        var customer = await repository.GetById(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    internal static async Task<IResult> Create(
        ICourseParticipantRepository repository, [FromBody]CourseParticipant courseParticipant)
    {
        await repository.Create(courseParticipant);
        return Results.Created($"/courseparticipant/{courseParticipant.CourseId}", courseParticipant);
    }

    internal static async Task<IResult> Update(ICourseParticipantRepository repository, long id, CourseParticipant courseParticipant)
    {
        var existing = await repository.GetById(courseParticipant.CourseId);
        if (existing is null)
        {
            return Results.NotFound();
        }

        await repository.Update(courseParticipant);
        return Results.Ok(courseParticipant);
    }

    internal static async Task<IResult> Delete(ICourseParticipantRepository repository, long id)
    {
        var deleted = await repository.Delete(id);
        return deleted ? Results.Ok() : Results.NotFound();
    }
}
