using System.Net;
using AutoMapper;
using Domain.DTOs.MentorGroupDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MentorGroupService;

public class MentorGroupService(DataContext context,IMapper mapper) : IMentorGroupService
{

    public async Task<Response<string>> AddMentorGroup(AddMentorGroupDto mentorGroup)
    {
        try
        {
            var mapped = mapper.Map<MentorGroup>(mentorGroup);
            await context.MentorGroups.AddAsync(mapped);
            var result = await context.SaveChangesAsync();

            if (result > 0) return new Response<string>("Successfully added");
            return new Response<string>("Error in adding");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetMentorGroupDto>>> GetMentorGroups(PaginationFilter filter)
    {
        try
        {
            var mentorGroups = context.MentorGroups.AsQueryable();

            var response = await mentorGroups
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).Select(x => new GetMentorGroupDto
                {
                    Group = x.Group,
                    GroupId = x.GroupId,
                    Mentor = x.Mentor,
                    MentorId = x.MentorId,
                }).ToListAsync();
            var totalRecord = mentorGroups.Count();

            return new PagedResponse<List<GetMentorGroupDto>>(response, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetMentorGroupDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> DeleteMentorGroup(int id)
    {
        try
        {
            var existing = await context.MentorGroups.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.BadRequest, "MentorGroup not found");
            context.MentorGroups.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
        catch (System.Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateMentorGroup(UpdateMentorGroupDto mentorGroup)
    {
        try
        {
            var mapped = mapper.Map<MentorGroup>(mentorGroup);
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
