using Domain.DTOs.TimeTableDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.TimeTableService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeTableController(ITimeTableService _timeTableService) : ControllerBase
{

    [HttpGet("Get-TimeTables")]
    public async Task<Response<List<GetTimeTableDto>>> GetTimeTables([FromQuery] TimeTableFilter filter)
    {
        return await _timeTableService.GetTimeTable(filter);
    }

    [HttpPost("Add-TimeTable")]
    public async Task<Response<string>> AddTimeTable(AddTimeTableDto timeTable)
    {
        return await _timeTableService.AddTimeTable(timeTable);
    }

    [HttpPut("Update-TimeTable")]
    public async Task<Response<string>> UpdateTimeTable(UpdateTimeTableDto timeTable)
    {
        return await _timeTableService.UpdateTimeTable(timeTable);
    }

    [HttpDelete("Delete-TimeTable")]
    public async Task<Response<string>> DeleteTimeTable(int id)
    {
        return await _timeTableService.DeleteTimeTable(id);
    }
}
