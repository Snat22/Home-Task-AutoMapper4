using Domain.DTOs.StudentGroupDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.StudentGroupService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentGroupController(IStudentGroupService _studentGroupService) : ControllerBase
{

    [HttpGet("Get-StudentGroups")]
    public async Task<PagedResponse<List<GetStudentGroupDto>>> GetStudentGroups([FromQuery] PaginationFilter filter)
    {
        return await _studentGroupService.GetStudentGroups(filter);
    }

    [HttpPost("Add-StudentGroup")]
    public async Task<Response<string>> AddStudentGroup(AddStudentGroupDto studentGroup)
    {
        return await _studentGroupService.AddStudentGroup(studentGroup);
    }

    [HttpPut("Update-StudentGroup")]
    public async Task<Response<string>> UpdateStudentGroup(UpdateStudentGroupDto studentGroup)
    {
        return await _studentGroupService.UpdateStudentGroup(studentGroup);
    }

    [HttpDelete("Delete-StudentGroup")]
    public async Task<Response<string>> DeleteStudentGroup(int id)
    {
        return await _studentGroupService.DeleteStudentGroup(id);
    }

}

