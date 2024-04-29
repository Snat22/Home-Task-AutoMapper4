using System.Net;
using AutoMapper;
using Domain.DTOs.TimeTableDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Services.TimeTableService;

public class TimeTableService(DataContext context,IMapper mapper) : ITimeTableService
{
    public async Task<Response<string>> AddTimeTable(AddTimeTableDto timeTable)
    {
        try
        {
            var mapped = mapper.Map<TimeTable>(timeTable);
            await context.TimeTable.AddAsync(mapped);
            var result = await context.SaveChangesAsync();

            if (result > 0) return new Response<string>("Successfully added");
            return new Response<string>("Error in adding");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetTimeTableDto>>> GetTimeTable(TimeTableFilter filter)
    {
        try
        {
            var TimeTables = context.TimeTable.AsQueryable();

            if (filter.DayOfWeek != null)
                TimeTables = TimeTables.Where(x => x.DayOfWeek == filter.DayOfWeek);

            var response = await TimeTables
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = TimeTables.Count();

            var mapped = mapper.Map<List<GetTimeTableDto>>(response);
            return new PagedResponse<List<GetTimeTableDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetTimeTableDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteTimeTable(int id)
    {
        try
        {
            var existing = await context.TimeTable.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.BadRequest, "TimeTable not found");
            context.TimeTable.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
        catch (System.Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateTimeTable(UpdateTimeTableDto TimeTable)
    {
        try
        {
            var mapped = mapper.Map<TimeTable>(TimeTable);
            context.Update(mapped);
            var res = await context.SaveChangesAsync();

            if (res > 0) return new Response<string>("Successfully updated");
            return new Response<string>("Error in updating");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

}
