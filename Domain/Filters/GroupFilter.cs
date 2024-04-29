namespace Domain.Filters;

public class GroupFilter : PaginationFilter
{
    public string? GroupName { get; set; }
    public string? Description { get; set; }
}
