using Domain.DTOs.MentorDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.MentorService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MentorController(IMentorService _mentorService) : ControllerBase
{

    [HttpGet("Get-Mentors")]
    public async Task<Response<List<GetMentorsDto>>> GetMentors([FromQuery] MentorFilter filter)
    {
        return await _mentorService.GetMentors(filter);
    }

    [HttpPost("Add-Mentor")]
    public async Task<Response<string>> AddMentor(AddMentorDto mentor)
    {
        return await _mentorService.AddMentor(mentor);
    }

    [HttpPut("Update-Mentor")]
    public async Task<Response<string>> UpdateMentor(UpdateMentorDto mentor)
    {
        return await _mentorService.UpdateMentor(mentor);
    }

    [HttpDelete("Delete-Mentor")]
    public async Task<Response<string>> DeleteMentor(int id)
    {
        return await _mentorService.DeleteMentor(id);
    }

    [HttpGet("Get-Mentors-withManyGroups")]
    public async Task<PagedResponse<List<Mentor>>> GetMentor_WithManyGroups([FromQuery] MentorFilter filter)
    {
        return await _mentorService.GetMentor_WithManyGroups(filter);
    }


    [HttpGet("Get-Mentors-by-dayOfWeek")]
    public async Task<PagedResponse<List<GetMentorsDto>>> GetMentors_ByDayOfWeek([FromQuery] MentorFilter filter)
    {
        return await _mentorService.GetMentors_ByDayOfWeek(filter);
    }

}
