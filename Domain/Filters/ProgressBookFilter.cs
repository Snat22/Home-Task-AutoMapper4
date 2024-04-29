namespace Domain.Filters;

public class ProgressBookFilter : PaginationFilter
{
    public int? Grade { get; set; }
    public bool? IsAttended { get; set; }
}
