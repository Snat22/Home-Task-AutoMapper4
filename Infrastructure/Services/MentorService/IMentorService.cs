using Domain.DTOs.MentorDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.MentorService;

public interface IMentorService
{
    public Task<PagedResponse<List<GetMentorsDto>>> GetMentors(MentorFilter filter);
    public Task<Response<string>> AddMentor(AddMentorDto mentor);
    public Task<Response<string>> UpdateMentor(UpdateMentorDto mentor);
    public Task<Response<string>> DeleteMentor(int id);
    public Task<PagedResponse<List<Mentor>>> GetMentor_WithManyGroups(MentorFilter filter);

    public Task<PagedResponse<List<GetMentorsDto>>> GetMentors_ByDayOfWeek(MentorFilter filter);
}
