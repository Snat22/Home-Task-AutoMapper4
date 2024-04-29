using System.Net;
using AutoMapper;
using Domain.DTOs.GroupDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.GroupService;

public class GroupService(DataContext context,IMapper mapper) : IGroupService
{

    public async Task<Response<string>> AddGroup(AddGroupDto group)
    {
        try
        {
            var found = await context.Groups.FirstOrDefaultAsync(x => x.GroupName == group.GroupName);
            if (found != null) return new Response<string>("The group is already exists");

            var mapped = mapper.Map<Group>(group);
            await context.Groups.AddAsync(mapped);
            var result = await context.SaveChangesAsync();

            if (result > 0) return new Response<string>("Successfully added");
            return new Response<string>("Error in adding");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetGroupsDto>>> GetGroups(GroupFilter filter)
    {
        try
        {
            var groups = context.Groups.AsQueryable();

            if (!string.IsNullOrEmpty(filter.GroupName))
                groups = groups.Where(x => x.GroupName.ToLower().Contains(filter.GroupName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Description.ToString()))
                groups = groups.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));

            var response = await groups
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = groups.Count();

            var mapped = mapper.Map<List<GetGroupsDto>>(response);
            return new PagedResponse<List<GetGroupsDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetGroupsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> DeleteGroup(int id)
    {
        try
        {
            var existing = await context.Groups.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.BadRequest, "Group not found");
            context.Groups.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
        catch (System.Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateGroup(UpdateGroupDto group)
    {
        try
        {
            var mapped = mapper.Map<Group>(group);
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

    // 1
    public async Task<PagedResponse<List<Group_StudentsCount>>> GetGroups_StudentCount(GroupFilter filter)
    {
        try
        {
            var groups = (from g in context.Groups
                          join sg in context.StudentGroups on g.Id equals sg.GroupId
                          join s in context.Students on sg.StudentId equals s.Id
                          select new Group_StudentsCount
                          {
                              Group = g,
                              StudentCount = context.StudentGroups.Where(x => x.GroupId == g.Id).Count(x => x.StudentId == s.Id),
                          }).AsQueryable();

            if (!string.IsNullOrEmpty(filter.GroupName))
                groups = groups.Where(x => x.Group.GroupName.ToLower().Contains(filter.GroupName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Description.ToString()))
                groups = groups.Where(x => x.Group.Description.ToLower().Contains(filter.Description.ToLower()));

            var response = await groups
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = groups.Count();

            var mapped = mapper.Map<List<Group_StudentsCount>>(response);
            return new PagedResponse<List<Group_StudentsCount>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<Group_StudentsCount>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    // 3
    public async Task<PagedResponse<List<Group>>> GetGroups_WithStudent(GroupFilter filter)
    {
        try
        {
            var groups = (from g in context.Groups
                          join sg in context.StudentGroups on g.Id equals sg.GroupId
                          join s in context.Students on sg.StudentId equals s.Id
                          select new
                          {
                              Group = g,
                              StudentCount = context.StudentGroups.Where(x => x.GroupId == g.Id && s.Status == Domain.Enums.Status.Active).Count(x => x.StudentId == s.Id),
                          }).Where(x => x.StudentCount >= 1).Select(x => x.Group).Distinct().AsQueryable();

            if (!string.IsNullOrEmpty(filter.GroupName))
                groups = groups.Where(x => x.GroupName.ToLower().Contains(filter.GroupName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Description.ToString()))
                groups = groups.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));

            var response = await groups
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = groups.Count();

            var mapped = mapper.Map<List<Group>>(response);
            return new PagedResponse<List<Group>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<Group>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
