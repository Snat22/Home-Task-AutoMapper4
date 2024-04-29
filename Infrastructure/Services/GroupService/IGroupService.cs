using Domain.DTOs.GroupDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.GroupService;

public interface IGroupService
{
    public Task<PagedResponse<List<GetGroupsDto>>> GetGroups(GroupFilter filter);
    public Task<Response<string>> AddGroup(AddGroupDto group);
    public Task<Response<string>> UpdateGroup(UpdateGroupDto group);
    public Task<Response<string>> DeleteGroup(int id);
    public Task<PagedResponse<List<Group_StudentsCount>>> GetGroups_StudentCount(GroupFilter filter);
    public Task<PagedResponse<List<Group>>> GetGroups_WithStudent(GroupFilter filter);
}
