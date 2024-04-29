using System.Net;
using AutoMapper;
using Domain.DTOs.MentorDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MentorService;

public class MentorService(DataContext context,IMapper mapper) : IMentorService
{

    public async Task<Response<string>> AddMentor(AddMentorDto mentor)
    {
        try
        {
            var mapped = mapper.Map<Mentor>(mentor);
            await context.Mentors.AddAsync(mapped);
            var result = await context.SaveChangesAsync();

            if (result > 0) return new Response<string>("Successfully added");
            return new Response<string>("Error in adding");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetMentorsDto>>> GetMentors(MentorFilter filter)
    {
        try
        {
            var mentors = context.Mentors.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                mentors = mentors.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                mentors = mentors.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await mentors
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = mentors.Count();

            var mapped = mapper.Map<List<GetMentorsDto>>(response);
            return new PagedResponse<List<GetMentorsDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetMentorsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> DeleteMentor(int id)
    {
        try
        {
            var existing = await context.Mentors.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.BadRequest, "Mentor not found");
            context.Mentors.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
        catch (System.Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateMentor(UpdateMentorDto mentor)
    {
        try
        {
            var mapped = mapper.Map<Mentor>(mentor);
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

    public async Task<PagedResponse<List<Mentor>>> GetMentor_WithManyGroups(MentorFilter filter)
    {
        try
        {
            var mentors = (from m in context.Mentors
                           join mg in context.MentorGroups on m.Id equals mg.MentorId
                           join g in context.Groups on mg.GroupId equals g.Id
                           select new
                           {
                               Mentor = m,
                               GroupCount = context.MentorGroups.Where(x => x.MentorId == m.Id).Count()
                           }).Where(x => x.GroupCount > 1).Select(x => x.Mentor).Distinct().AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                mentors = mentors.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                mentors = mentors.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await mentors
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = mentors.Count();

            var mapped = mapper.Map<List<Mentor>>(response);
            return new PagedResponse<List<Mentor>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<Mentor>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }



    public async Task<PagedResponse<List<GetMentorsDto>>> GetMentors_ByDayOfWeek(MentorFilter filter)
    {
        try
        {
            var students = (from m in context.Mentors
                            join mg in context.MentorGroups on m.Id equals mg.MentorId
                            join g in context.Groups on mg.GroupId equals g.Id
                            join t in context.TimeTable on g.Id equals t.GroupId
                            where t.DayOfWeek == DayOfWeek.Monday
                            select new
                            {
                                Mentor = m
                            }).Select(x => x.Mentor).Distinct().AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                students = students.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                students = students.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<GetMentorsDto>>(response);
            return new PagedResponse<List<GetMentorsDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (System.Exception ex)
        {
            return new PagedResponse<List<GetMentorsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
