using Domain.DTOs.ProgressBookDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.ProgressBookService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProgressBookController(IProgressBookService _progressBookService) : ControllerBase
{
    [HttpGet("Get-ProgressBooks")]
    public async Task<Response<List<GetProgressBookDto>>> GetProgressBooks([FromQuery] ProgressBookFilter filter)
    {
        return await _progressBookService.GetProgressBook(filter);
    }

    [HttpPost("Add-ProgressBook")]
    public async Task<Response<string>> AddProgressBook(AddProgressBookDto progressBook)
    {
        return await _progressBookService.AddProgressBook(progressBook);
    }

    [HttpPut("Update-ProgressBook")]
    public async Task<Response<string>> UpdateProgressBook(UpdateProgressBookDto progressBook)
    {
        return await _progressBookService.UpdateProgressBook(progressBook);
    }

    [HttpDelete("Delete-ProgressBook")]
    public async Task<Response<string>> DeleteProgressBook(int id)
    {
        return await _progressBookService.DeleteProgressBook(id);
    }
}
