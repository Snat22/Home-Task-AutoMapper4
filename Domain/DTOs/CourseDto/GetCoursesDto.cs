using System.Text.RegularExpressions;
using Domain.Enums;

namespace Domain.DTOs.CourseDto;

public class GetCoursesDto
{
    public int Id { get; set; }
    public string? CourseName { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
}
