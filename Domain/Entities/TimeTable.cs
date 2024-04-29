using System.ComponentModel;
using System.Text.RegularExpressions;
using Domain.Enums;
using Microsoft.VisualBasic;

namespace Domain.Entities;

public class TimeTable : BaseEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
    public TimeTableType TimeTableType { get; set; }

    public int GroupId { get; set; }
    public virtual Group? Group { get; set; }
    public List<ProgressBook>? ProgressBook { get; set; }

}
