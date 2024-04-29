using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.StudentServices;

public interface IStudentService
{
    public Task<Response<string>> AddStudent(AddStudentDto student);
    public Task<PagedResponse<List<GetStudentsDto>>> GetStudents(StudentFilter filter);
    public Task<Response<string>> UpdateStudent(UpdateStudentDto student);
    public Task<Response<string>> DeleteStudent(int id);
    public Task<PagedResponse<List<GetStudentsDto>>> GetStudents_WithoutGroup(StudentFilter filter);
    public Task<PagedResponse<List<StudentAndMentor_Group>>> GetStudentsMentors_Group(StudentFilter filter);
    public Task<PagedResponse<List<GetStudentsDto>>> GetStudentsByGroupName(StudentFilter filter);
    public Task<PagedResponse<List<GetStudentsDto>>> GetStudents_DidNotVisit(StudentFilter filter);
    public Task<PagedResponse<List<GetStudentsDto>>> GetStudents_HavenotPractice(StudentFilter filter);
    public Task<PagedResponse<List<GetStudentsDto>>> GetStudents_DidNotVisitInDayOfWeek(StudentFilter filter);


    public Task<PagedResponse<List<GetStudentsDto>>> GetStudents_WithGreatGrade(StudentFilter filter);

}
