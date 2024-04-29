using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.DTOs.TimeTableDto;

public class AddTimeTableDto
{
    public DayOfWeek DayOfWeek { get; set; }
    [RegularExpression(@"^(0[0-9]|1[0-9]|2[0-3]|[0-9]):[0-5][0-9]$", ErrorMessage = "Use format HH:MM only")]
    public string? FromTime { get; set; }
    [RegularExpression(@"^(0[0-9]|1[0-9]|2[0-3]|[0-9]):[0-5][0-9]$", ErrorMessage = "Use format HH:MM only")]
    public string? ToTime { get; set; }
    public TimeTableType TimeTableType { get; set; }
    public int GroupId { get; set; }

}
