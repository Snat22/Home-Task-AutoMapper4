using Domain.Enums;

namespace Domain.DTOs.TimeTableDto;

public class GetTimeTableDto
{
    public int Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
    public TimeTableType TimeTableType { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public int GroupId { get; set; }

}
