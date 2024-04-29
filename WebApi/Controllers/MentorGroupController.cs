using Domain.DTOs.MentorGroupDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.MentorGroupService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MentorGroupController(IMentorGroupService _mentorGroupService) : ControllerBase
{

    [HttpGet("Get-MentorGroups")]
    public async Task<PagedResponse<List<GetMentorGroupDto>>> GetMentorGroups([FromQuery] PaginationFilter filter)
    {
        return await _mentorGroupService.GetMentorGroups(filter);
    }

    [HttpPost("Add-MentorGroup")]
    public async Task<Response<string>> AddMentorGroup(AddMentorGroupDto mentorGroup)
    {
        return await _mentorGroupService.AddMentorGroup(mentorGroup);
    }

    [HttpPut("Update-MentorGroup")]
    public async Task<Response<string>> UpdateMentorGroup(UpdateMentorGroupDto mentorGroup)
    {
        return await _mentorGroupService.UpdateMentorGroup(mentorGroup);
    }

    [HttpDelete("Delete-MentorGroup")]
    public async Task<Response<string>> DeleteMentorGroup(int id)
    {
        return await _mentorGroupService.DeleteMentorGroup(id);
    }

}
