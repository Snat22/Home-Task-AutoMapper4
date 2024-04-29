using Domain.Enums;

namespace Domain.Filters;

public class CourseFilter : PaginationFilter
{
    public string? CourseName { get; set; }
    public string? Description { get; set; }
}
