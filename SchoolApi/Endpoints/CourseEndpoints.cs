using SchoolApi.Entities;
using SchoolApi.Repositories;

namespace SchoolApi.Endpoints;
public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this WebApplication app)
    {
        app.MapGet("course", GetAll);
        app.MapGet("course/{id:long}", GetById);
        app.MapPost("course", Create);
        app.MapPut("course/{id:long}", Update);
        app.MapDelete("course/{id:long}", Delete);
    }

    internal static async Task<IEnumerable<Course>> GetAll(ICourseRepository repository)
    {
       return await repository.GetAllAsync();
    }

    internal static async Task<IResult> GetById(ICourseRepository repository, long id)
    {
        var customer = await repository.GetById(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    internal static async Task<IResult> Create(
        ICourseRepository repository, Course course)
    {
        await repository.Create(course);
        return Results.Created($"/customers/{course.Id}", course);
    }

    internal static async Task<IResult> Update(ICourseRepository repository, long id, Course course)
    {
        var existing = await repository.GetById(course.Id);
        if (existing is null)
        {
            return Results.NotFound();
        }

        await repository.Update(course);
        return Results.Ok(course);
    }

    internal static async Task<IResult> Delete(ICourseRepository repository, long id)
    {
        var deleted = await repository.Delete(id);
        return deleted ? Results.Ok() : Results.NotFound();
    }
}