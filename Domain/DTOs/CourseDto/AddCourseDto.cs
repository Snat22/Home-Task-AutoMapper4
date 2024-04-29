using Domain.Enums;

namespace Domain.DTOs.CourseDto;

public class AddCourseDto
{
    public required string CourseName { get; set; } = null!;
    public required string Description { get; set; }
    public Status Status { get; set; }
}
