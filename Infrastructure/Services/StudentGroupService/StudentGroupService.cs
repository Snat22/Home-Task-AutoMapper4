using System.Net;
using AutoMapper;
using Domain.DTOs.StudentGroupDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StudentGroupService;

public class StudentGroupService(DataContext context,IMapper mapper) : IStudentGroupService
{

    public async Task<Response<string>> AddStudentGroup(AddStudentGroupDto studentGroup)
    {
        try
        {
            var mapped = mapper.Map<StudentGroup>(studentGroup);
            await context.StudentGroups.AddAsync(mapped);
            var result = await context.SaveChangesAsync();

            if (result > 0) return new Response<string>("Successfully added");
            return new Response<string>("Error in adding");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetStudentGroupDto>>> GetStudentGroups(PaginationFilter filter)
    {
        try
        {
            var studentGroups = context.StudentGroups.AsQueryable();

            var response = await studentGroups
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = studentGroups.Count();

            var mapped = mapper.Map<List<GetStudentGroupDto>>(response);
            return new PagedResponse<List<GetStudentGroupDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetStudentGroupDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> DeleteStudentGroup(int id)
    {
        try
        {
            var existing = await context.StudentGroups.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.BadRequest, "StudentGroup not found");
            context.StudentGroups.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
        catch (System.Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateStudentGroup(UpdateStudentGroupDto studentGroup)
    {
        try
        {
            var mapped = mapper.Map<StudentGroup>(studentGroup);
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
