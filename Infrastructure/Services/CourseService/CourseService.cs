using System.Data;
using System.Net;
using AutoMapper;
using Domain.DTOs.CourseDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CourseService;

public class CourseService(DataContext context,IMapper mapper) : ICourseService
{
    public async Task<Response<string>> AddCourse(AddCourseDto course)
    {
        try
        {
            var found = await context.Courses.FirstOrDefaultAsync(x => x.CourseName == course.CourseName);
            if (found != null) return new Response<string>("The course is already exists");

            var mapped = mapper.Map<Course>(course);
            await context.Courses.AddAsync(mapped);
            var result = await context.SaveChangesAsync();

            if (result > 0) return new Response<string>("Successfully added");
            return new Response<string>("Error in adding");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses(CourseFilter filter)
    {
        try
        {
            var courses = context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(filter.CourseName))
                courses = courses.Where(x => x.CourseName.ToLower().Contains(filter.CourseName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Description.ToString()))
                courses = courses.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));

            var response = await courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = courses.Count();

            var mapped = mapper.Map<List<GetCoursesDto>>(response);
            return new PagedResponse<List<GetCoursesDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetCoursesDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> DeleteCourse(int id)
    {
        try
        {
            var existing = await context.Courses.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.BadRequest, "Course not found");
            context.Courses.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
        catch (System.Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateCourse(UpdateCourseDto course)
    {
        try
        {
            var mapped = mapper.Map<Course>(course);
            context.Update(mapped);
            var res = await context.SaveChangesAsync();

            if (res > 0) return new Response<string>("Successfully updated");
            return new Response<string>("Error in updating");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses_ActiveStudent(CourseFilter filter)
    {
        try
        {
            var courses = (from c in context.Courses
                        join g in context.Groups on c.Id equals g.CourseId
                        join sg in context.StudentGroups on g.Id equals sg.GroupId
                        join s in context.Students on sg.StudentId equals s.Id
                        where s.Status == Domain.Enums.Status.Active
                        select new
                        {
                            Course = c,
                        }).Select(x => x.Course).AsQueryable();

            if (!string.IsNullOrEmpty(filter.CourseName))
                courses = courses.Where(x => x.CourseName.ToLower().Contains(filter.CourseName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Description))
                courses = courses.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));

            var response = await courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = courses.Count();

            var mapped = mapper.Map<List<GetCoursesDto>>(response);
            return new PagedResponse<List<GetCoursesDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetCoursesDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses_StudentName(CourseFilter filter)
    {
        try
        {
            var courses = (from c in context.Courses
                           join g in context.Groups on c.Id equals g.CourseId
                           join sg in context.StudentGroups on g.Id equals sg.GroupId
                           join s in context.Students on sg.StudentId equals s.Id
                           where s.FirstName.ToLower().StartsWith("s")
                           select new
                           {
                               Course = c,
                           }).Select(x => x.Course).AsQueryable();

            if (!string.IsNullOrEmpty(filter.CourseName))
                courses = courses.Where(x => x.CourseName.ToLower().Contains(filter.CourseName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Description))
                courses = courses.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));

            var response = await courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = courses.Count();

            var mapped = mapper.Map<List<GetCoursesDto>>(response);
            return new PagedResponse<List<GetCoursesDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetCoursesDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses_StudentAllGender(CourseFilter filter)
    {
        try
        {
            var courses = (from c in context.Courses
                           join g in context.Groups on c.Id equals g.CourseId
                           join sg in context.StudentGroups on g.Id equals sg.GroupId
                           join s in context.Students on sg.StudentId equals s.Id
                           select new
                           {
                               Course = c,
                               MaleCount = context.Students.Where(x => x.Id == sg.StudentId).Count(x => x.Gender == Domain.Enums.Gender.Male),
                               FemaleCount = context.Students.Where(x => x.Id == sg.StudentId).Count(x => x.Gender == Domain.Enums.Gender.Female),
                           }).Where(x => x.MaleCount != 0 && x.FemaleCount != 0).Select(x => x.Course).AsQueryable();

            if (!string.IsNullOrEmpty(filter.CourseName))
                courses = courses.Where(x => x.CourseName.ToLower().Contains(filter.CourseName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Description))
                courses = courses.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));

            var response = await courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = courses.Count();

            var mapped = mapper.Map<List<GetCoursesDto>>(response);
            return new PagedResponse<List<GetCoursesDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetCoursesDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses_WhereMaleMoreThanFemale(CourseFilter filter)
    {
        try
        {
            var courses = (from c in context.Courses
                           join g in context.Groups on c.Id equals g.CourseId
                           join sg in context.StudentGroups on g.Id equals sg.GroupId
                           join s in context.Students on sg.StudentId equals s.Id
                           select new
                           {
                               Course = c,
                               MaleCount = context.Students.Where(x => x.Id == sg.StudentId).Count(x => x.Gender == Domain.Enums.Gender.Male),
                               FemaleCount = context.Students.Where(x => x.Id == sg.StudentId).Count(x => x.Gender == Domain.Enums.Gender.Female),
                           }).Where(x => x.MaleCount > x.FemaleCount).Select(x => x.Course).AsQueryable();

            if (!string.IsNullOrEmpty(filter.CourseName))
                courses = courses.Where(x => x.CourseName.ToLower().Contains(filter.CourseName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Description))
                courses = courses.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));

            var response = await courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = courses.Count();

            var mapped = mapper.Map<List<GetCoursesDto>>(response);
            return new PagedResponse<List<GetCoursesDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetCoursesDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses_ByDoB(CourseFilter filter)
    {
        try
        {
            var date = new DateTime(2024, 4, 27);
            var courses = (from c in context.Courses
                           join g in context.Groups on c.Id equals g.CourseId
                           join sg in context.StudentGroups on g.Id equals sg.GroupId
                           join s in context.Students on sg.StudentId equals s.Id
                           where date.Year - s.DoB.Year > 25
                           select new
                           {
                               Course = c,
                           }).Select(x => x.Course).AsQueryable();

            if (!string.IsNullOrEmpty(filter.CourseName))
                courses = courses.Where(x => x.CourseName.ToLower().Contains(filter.CourseName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Description))
                courses = courses.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));

            var response = await courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = courses.Count();

            var mapped = mapper.Map<List<GetCoursesDto>>(response);
            return new PagedResponse<List<GetCoursesDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetCoursesDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

}
