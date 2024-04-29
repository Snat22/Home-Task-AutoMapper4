using Domain.DTOs.MentorGroupDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.MentorGroupService;

public interface IMentorGroupService
{
    public Task<PagedResponse<List<GetMentorGroupDto>>> GetMentorGroups(PaginationFilter filter);
    public Task<Response<string>> AddMentorGroup(AddMentorGroupDto mentorGroup);
    public Task<Response<string>> UpdateMentorGroup(UpdateMentorGroupDto mentorGroup);
    public Task<Response<string>> DeleteMentorGroup(int id);
}
