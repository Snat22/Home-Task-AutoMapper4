using Domain.DTOs.CourseDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.CourseService;

public interface ICourseService
{
    public Task<PagedResponse<List<GetCoursesDto>>> GetCourses(CourseFilter filter);
    public Task<Response<string>> AddCourse(AddCourseDto course);
    public Task<Response<string>> UpdateCourse(UpdateCourseDto course);
    public Task<Response<string>> DeleteCourse(int id);
    public Task<PagedResponse<List<GetCoursesDto>>> GetCourses_ActiveStudent(CourseFilter filter);


    // last tasks
    public Task<PagedResponse<List<GetCoursesDto>>> GetCourses_StudentName(CourseFilter filter);
    public Task<PagedResponse<List<GetCoursesDto>>> GetCourses_StudentAllGender(CourseFilter filter);
    public Task<PagedResponse<List<GetCoursesDto>>> GetCourses_WhereMaleMoreThanFemale(CourseFilter filter);
    public Task<PagedResponse<List<GetCoursesDto>>> GetCourses_ByDoB(CourseFilter filter);

}