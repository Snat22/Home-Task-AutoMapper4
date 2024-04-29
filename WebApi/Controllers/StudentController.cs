using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.StudentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Tree;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController(IStudentService _studentService) : ControllerBase
{

    [HttpGet("get-students")]
    public async Task<Response<List<GetStudentsDto>>> GetStudents([FromQuery] StudentFilter filter)
    {
        return await _studentService.GetStudents(filter);
    }

    [HttpPost("add-student")]
    public async Task<Response<string>> AddStudent(AddStudentDto student)
    {
        return await _studentService.AddStudent(student);
    }

    [HttpPut("update-student")]
    public async Task<Response<string>> UpdateStudent(UpdateStudentDto student)
    {
        return await _studentService.UpdateStudent(student);
    }

    [HttpDelete("delete-student")]
    public async Task<Response<string>> DeleteStudent(int id)
    {
        return await _studentService.DeleteStudent(id);
    }

    [HttpGet("get-students-withoutGroup")]
    public async Task<Response<List<GetStudentsDto>>> GetStudents_WithoutGroup([FromQuery] StudentFilter filter)
    {
        return await _studentService.GetStudents_WithoutGroup(filter);
    }

    [HttpGet("get-Students-Mentors_Group")]
    public async Task<Response<List<StudentAndMentor_Group>>> GetStudentsMentors_Group([FromQuery] StudentFilter filter)
    {
        return await _studentService.GetStudentsMentors_Group(filter);
    }

    [HttpGet("get-Students-by-groupName")]
    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudentsByCourseName([FromQuery] StudentFilter filter)
    {
        return await _studentService.GetStudentsByGroupName(filter);
    }

    [HttpGet("get-Students-didnotvisit")]
    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudents_DidNotVisit([FromQuery] StudentFilter filter)
    {
        return await _studentService.GetStudents_DidNotVisit(filter);
    }

    [HttpGet("get-Students-haveNotPractice")]
    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudents_HavenotPractice([FromQuery] StudentFilter filter)
    {
        return await _studentService.GetStudents_HavenotPractice(filter);
    }

    [HttpGet("get-Students-didNotVisit-inDayOfWeek")]
    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudents_DidNotVisitInDayOfWeek([FromQuery] StudentFilter filter)
    {
        return await _studentService.GetStudents_DidNotVisitInDayOfWeek(filter);
    }

    [HttpGet("get-Students-With-Great-Grade")]
    public async Task<PagedResponse<List<GetStudentsDto>>> GetStudents_WithGreatGrade([FromQuery] StudentFilter filter)
    {
        return await _studentService.GetStudents_WithGreatGrade(filter);
    }
}
