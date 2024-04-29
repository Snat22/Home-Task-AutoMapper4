using Domain.DTOs.TimeTableDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.TimeTableService;

public interface ITimeTableService
{
    public Task<Response<string>> AddTimeTable(AddTimeTableDto timeTable);
    public Task<PagedResponse<List<GetTimeTableDto>>> GetTimeTable(TimeTableFilter filter);
    public Task<Response<string>> UpdateTimeTable(UpdateTimeTableDto timeTable);
    public Task<Response<string>> DeleteTimeTable(int id);
}
