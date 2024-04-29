using Domain.DTOs.CourseDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.CourseService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CourseController(ICourseService _courseService) : ControllerBase
{

    [HttpGet("Get-Courses")]
    public async Task<Response<List<GetCoursesDto>>> GetCourses([FromQuery] CourseFilter filter)
    {
        return await _courseService.GetCourses(filter);
    }

    [HttpPost("Add-Course")]
    public async Task<Response<string>> AddCourse(AddCourseDto course)
    {
        return await _courseService.AddCourse(course);
    }

    [HttpPut("Update-Course")]
    public async Task<Response<string>> UpdateCourse(UpdateCourseDto course)
    {
        return await _courseService.UpdateCourse(course);
    }

    [HttpDelete("Delete-Course")]
    public async Task<Response<string>> DeleteCourse(int id)
    {
        return await _courseService.DeleteCourse(id);
    }


    [HttpGet("Get-Courses-With-Active-Student")]
    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses_ActiveStudent([FromQuery] CourseFilter filter)
    {
        return await _courseService.GetCourses_ActiveStudent(filter);
    }

    [HttpGet("Get-Courses-StudentName")]
    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses_StudentName([FromQuery] CourseFilter filter)
    {
        return await _courseService.GetCourses_StudentName(filter);
    }

    [HttpGet("Get-Courses-AllStudentGender")]

    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses_StudentAllGender([FromQuery] CourseFilter filter)
    {
        return await _courseService.GetCourses_StudentAllGender(filter);
    }

    [HttpGet("Get-Courses-WhereMaleMoreThanFemale")]
    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses_WhereMaleMoreThanFemale([FromQuery] CourseFilter filter)
    {

        return await _courseService.GetCourses_WhereMaleMoreThanFemale(filter);
    }

    [HttpGet("Get-Courses-ByDoB")]
    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses_ByDoB([FromQuery] CourseFilter filter)
    {
        return await _courseService.GetCourses_ByDoB(filter);
    }


}
