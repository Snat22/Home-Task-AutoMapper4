using Domain.DTOs.ProgressBookDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.ProgressBookService;

public interface IProgressBookService
{
    public Task<Response<string>> AddProgressBook(AddProgressBookDto progressBook);
    public Task<PagedResponse<List<GetProgressBookDto>>> GetProgressBook(ProgressBookFilter filter);
    public Task<Response<string>> UpdateProgressBook(UpdateProgressBookDto progressBook);
    public Task<Response<string>> DeleteProgressBook(int id);
}
