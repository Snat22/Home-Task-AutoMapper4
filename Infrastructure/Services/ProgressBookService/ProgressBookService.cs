using System.Net;
using AutoMapper;
using Domain.DTOs.ProgressBookDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ProgressBookService;

public class ProgressBookService(DataContext context,IMapper mapper) : IProgressBookService
{
    public async Task<Response<string>> AddProgressBook(AddProgressBookDto progressBook)
    {
        try
        {
            var mapped = mapper.Map<ProgressBook>(progressBook);
            await context.ProgressBook.AddAsync(mapped);
            var result = await context.SaveChangesAsync();

            if (result > 0) return new Response<string>("Successfully added");
            return new Response<string>("Error in adding");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetProgressBookDto>>> GetProgressBook(ProgressBookFilter filter)
    {
        try
        {
            var progressBooks = context.ProgressBook.AsQueryable();

            if (filter.Grade != null)
                progressBooks = progressBooks.Where(x => x.Grade == filter.Grade);
            if (filter.IsAttended != null)
                progressBooks = progressBooks.Where(x => x.IsAttended == filter.IsAttended);

            var response = await progressBooks
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = progressBooks.Count();

            var mapped = mapper.Map<List<GetProgressBookDto>>(response);
            return new PagedResponse<List<GetProgressBookDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetProgressBookDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteProgressBook(int id)
    {
        try
        {
            var existing = await context.ProgressBook.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.BadRequest, "ProgressBook not found");
            context.ProgressBook.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
        catch (System.Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateProgressBook(UpdateProgressBookDto ProgressBook)
    {
        try
        {
            var mapped = mapper.Map<ProgressBook>(ProgressBook);
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
