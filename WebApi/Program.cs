using Infrastructure.AutoMapper;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.CourseService;
using Infrastructure.Services.GroupService;
using Infrastructure.Services.MentorGroupService;
using Infrastructure.Services.MentorService;
using Infrastructure.Services.ProgressBookService;
using Infrastructure.Services.StudentGroupService;
using Infrastructure.Services.StudentServices;
using Infrastructure.Services.TimeTableService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(x => x.UseNpgsql(connection));

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IMentorService, MentorService>();
builder.Services.AddScoped<IStudentGroupService, StudentGroupService>();
builder.Services.AddScoped<IMentorGroupService, MentorGroupService>();
builder.Services.AddScoped<IProgressBookService, ProgressBookService>();
builder.Services.AddScoped<ITimeTableService, TimeTableService>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

