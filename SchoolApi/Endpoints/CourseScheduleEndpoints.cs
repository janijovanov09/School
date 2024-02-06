using Microsoft.AspNetCore.Mvc;
using SchoolApi.Entities;
using SchoolApi.Repositories;

namespace SchoolApi.Endpoints;

public static class CourseScheduleEndpoints
{
    public static void MapCourseScheduleEndpoints(this WebApplication app)
    {
        app.MapGet("courseschedule", GetAll);
        app.MapGet("courseschedule/{id:long}", GetById);
        app.MapPost("courseschedule", Create);
        app.MapPut("courseschedule/{id:long}", Update);
        app.MapDelete("courseschedule/{id:long}", Delete);
    }

    internal static async Task<IEnumerable<CourseSchedule>> GetAll(ICourseScheduleRepository repository)
    {
        return await repository.GetAllAsync();
    }

    internal static async Task<IResult> GetById(ICourseScheduleRepository repository, long id)
    {
        var customer = await repository.GetById(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    internal static async Task<IResult> Create(
        ICourseScheduleRepository repository, CourseSchedule courseSchedule)
    {
        await repository.Create(courseSchedule);
        return Results.Created($"/courseSchedule/{courseSchedule.CourseId}", courseSchedule);
    }

    internal static async Task<IResult> Update(ICourseScheduleRepository repository, long id, CourseSchedule courseSchedule)
    {
        var existing = await repository.GetById(courseSchedule.CourseId);
        if (existing is null)
        {
            return Results.NotFound();
        }

        await repository.Update(courseSchedule);
        return Results.Ok(courseSchedule);
    }

    internal static async Task<IResult> Delete(ICourseScheduleRepository repository, long id)
    {
        var deleted = await repository.Delete(id);
        return deleted ? Results.Ok() : Results.NotFound();
    }
}
