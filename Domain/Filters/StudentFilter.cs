using Domain.Enums;

namespace Domain.Filters;

public class StudentFilter : PaginationFilter
{
    public string? Address { get; set; }
    public string? Email { get; set; }
    public Status? Status { get; set; }
}
