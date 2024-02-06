using Microsoft.EntityFrameworkCore;
using SchoolApi.Database;
using SchoolApi.Endpoints;
using SchoolApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ReadJsonData>();
builder.Services.AddDbContext<SchoolDbContext>(options =>
        options.UseSqlite(config.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<IParticipantRepository, ParticipantRepository>();
builder.Services.AddTransient<ICourseParticipantRepository, CourseParticipantRepository>();
builder.Services.AddTransient<ICourseScheduleRepository, CourseScheduleRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapCourseEndpoints();
app.MapParticpantEndpoints();
app.MapCourseParticpantEndpoints();
app.MapCourseScheduleEndpoints();

app.Run();


