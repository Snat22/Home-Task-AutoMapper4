using Domain.DTOs.StudentGroupDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.StudentGroupService;

public interface IStudentGroupService
{
    public Task<PagedResponse<List<GetStudentGroupDto>>> GetStudentGroups(PaginationFilter filter);
    public Task<Response<string>> AddStudentGroup(AddStudentGroupDto studentGroup);
    public Task<Response<string>> UpdateStudentGroup(UpdateStudentGroupDto studentGroup);
    public Task<Response<string>> DeleteStudentGroup(int id);
}
