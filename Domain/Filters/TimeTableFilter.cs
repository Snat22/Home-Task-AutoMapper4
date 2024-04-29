namespace Domain.Filters;

public class TimeTableFilter : PaginationFilter
{
    public DayOfWeek? DayOfWeek { get; set; }
}
