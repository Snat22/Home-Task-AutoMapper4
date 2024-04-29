using Domain.Entities;

namespace Domain.DTOs.GroupDto;

public class Group_StudentsCount
{
    public Group? Group { get; set; }
    public int StudentCount { get; set; }
}
