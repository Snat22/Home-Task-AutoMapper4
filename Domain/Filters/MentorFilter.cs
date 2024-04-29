namespace Domain.Filters;

public class MentorFilter : PaginationFilter
{
    public string? Address { get; set; }
    public string? Email { get; set; }
}
