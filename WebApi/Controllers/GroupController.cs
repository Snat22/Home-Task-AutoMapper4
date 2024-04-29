using Domain.DTOs.GroupDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.GroupService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupController(IGroupService _groupService) : ControllerBase
{

    [HttpGet("Get-Groups")]
    public async Task<PagedResponse<List<GetGroupsDto>>> GetGroups([FromQuery] GroupFilter filter)
    {
        return await _groupService.GetGroups(filter);
    }

    [HttpPost("Add-Group")]
    public async Task<Response<string>> AddGroup(AddGroupDto group)
    {
        return await _groupService.AddGroup(group);
    }

    [HttpPut("Update-Group")]
    public async Task<Response<string>> UpdateGroup(UpdateGroupDto group)
    {
        return await _groupService.UpdateGroup(group);
    }

    [HttpDelete("Delete-Group")]
    public async Task<Response<string>> DeleteGroup(int id)
    {
        return await _groupService.DeleteGroup(id);
    }

    [HttpGet("Get-Groups-StudentsCount")]
    public async Task<Response<List<Group_StudentsCount>>> GetGroups_StudentCount([FromQuery] GroupFilter filter)
    {
        return await _groupService.GetGroups_StudentCount(filter);
    }

    [HttpGet("Get-Groups-WithStudent")]
    public async Task<Response<List<Group>>> GetGroups_WithStudent([FromQuery] GroupFilter filter)
    {
        return await _groupService.GetGroups_WithStudent(filter);
    }

}
