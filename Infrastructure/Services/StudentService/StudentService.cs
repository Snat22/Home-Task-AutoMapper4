using System.Net;
using AutoMapper;
using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Services.StudentServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StudentService(DataContext context,IMapper mapper) : IStudentService
{
    public async Task<Response<string>> AddStudent(AddStudentDto student)
    {
        try
        {
            var mapped = mapper.Map<Student>(student);
            await context.Students.AddAsync(mapped);
            var result = await context.SaveChangesAsync();

            if (result > 0) return new Response<string>("Successfully added");
            return new Response<string>("Error in adding");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudents(StudentFilter filter)
    {
        try
        {
            var students = context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                students = students.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                students = students.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));
            if (filter.Status != null)
                students = students.Where(x => x.Status == filter.Status);

            var response = await students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<GetStudentsDto>>(response);
            return new PagedResponse<List<GetStudentsDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetStudentsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteStudent(int id)
    {
        try
        {
            var existing = await context.Students.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.BadRequest, "Student not found");
            context.Students.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
        catch (System.Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateStudent(UpdateStudentDto student)
    {
        try
        {
            var mapped = mapper.Map<Student>(student);
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


    
    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudents_WithoutGroup(StudentFilter filter)
    {
        try
        {
            var students = (from s in context.Students
                            join sg in context.StudentGroups on s.Id equals sg.StudentId
                            join g in context.Groups on sg.GroupId equals g.Id
                            select new
                            {
                                Student = s,
                                StudentGroupCount = context.StudentGroups.Where(x => x.StudentId == s.Id).Count(x => x.GroupId == g.Id),
                            })
                               .Where(x => x.StudentGroupCount == 0).Select(x => x.Student).Distinct().AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                students = students.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                students = students.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<GetStudentsDto>>(response);
            return new PagedResponse<List<GetStudentsDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetStudentsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<PagedResponse<List<StudentAndMentor_Group>>> GetStudentsMentors_Group(StudentFilter filter)
    {
        try
        {
            var students = (from s in context.Students
                            join sg in context.StudentGroups on s.Id equals sg.StudentId
                            join g in context.Groups on sg.GroupId equals g.Id
                            join mg in context.MentorGroups on g.Id equals mg.GroupId
                            join m in context.Mentors on mg.MentorId equals m.Id
                            where g.Id == 5
                            select new StudentAndMentor_Group
                            {
                                Student = s,
                                Mentor = m,
                                Group = g,
                            }).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                students = students.Where(x => x.Student.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                students = students.Where(x => x.Student.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<StudentAndMentor_Group>>(response);
            return new PagedResponse<List<StudentAndMentor_Group>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<StudentAndMentor_Group>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudentsByGroupName(StudentFilter filter)
    {
        try
        {
            var students = (from s in context.Students
                            join sg in context.StudentGroups on s.Id equals sg.StudentId
                            join g in context.Groups on sg.GroupId equals g.Id
                            join c in context.Courses on g.CourseId equals c.Id
                            where c.CourseName == "string1"
                            select new
                            {
                                Student = s,
                            }).Select(x => x.Student).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                students = students.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                students = students.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<GetStudentsDto>>(response);
            return new PagedResponse<List<GetStudentsDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetStudentsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudents_DidNotVisit(StudentFilter filter)
    {
        try
        {
            var students = (from s in context.Students
                            join pb in context.ProgressBook on s.Id equals pb.StudentId
                            select new
                            {
                                Student = s,
                                ProgressBook = pb,
                                ProgressCount = context.ProgressBook.Count(x => x.StudentId == s.Id),
                                NotComingProgressCount = context.ProgressBook.Where(x => x.IsAttended == false).Count(x => x.StudentId == s.Id),
                            }).Where(x => x.ProgressCount == x.NotComingProgressCount).Select(x => x.Student).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                students = students.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                students = students.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<GetStudentsDto>>(response);
            return new PagedResponse<List<GetStudentsDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetStudentsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudents_HavenotPractice(StudentFilter filter)
    {
        try
        {
            var students = (from s in context.Students
                            join pb in context.ProgressBook on s.Id equals pb.StudentId
                            join tt in context.TimeTable on pb.TimeTableId equals tt.Id
                            where tt.TimeTableType != Domain.Enums.TimeTableType.Practice
                            select new
                            {
                                Student = s,
                            }).Select(x => x.Student).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                students = students.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                students = students.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<GetStudentsDto>>(response);
            return new PagedResponse<List<GetStudentsDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetStudentsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudents_DidNotVisitInDayOfWeek(StudentFilter filter)
    {
        try
        {
            var students = (from s in context.Students
                            join pb in context.ProgressBook on s.Id equals pb.StudentId
                            join tt in context.TimeTable on pb.TimeTableId equals tt.Id
                            where pb.IsAttended == false && tt.DayOfWeek == DayOfWeek.Monday
                            select new
                            {
                                Student = s,
                            }).Select(x => x.Student).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                students = students.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                students = students.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<GetStudentsDto>>(response);
            return new PagedResponse<List<GetStudentsDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetStudentsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudents_WithGreatGrade(StudentFilter filter)
    {
        try
        {
            var students = (from s in context.Students
                            join pb in context.ProgressBook on s.Id equals pb.StudentId
                            where pb.Grade >= 90
                            select new
                            {
                                Student = s,
                            }).Select(x => x.Student).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                students = students.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                students = students.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<GetStudentsDto>>(response);
            return new PagedResponse<List<GetStudentsDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetStudentsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

}

