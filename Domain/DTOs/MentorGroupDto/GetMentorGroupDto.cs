
using Domain.Entities;

namespace Domain.DTOs.MentorGroupDto;

public class GetMentorGroupDto
{
    public int MentorId { get; set; }
    public Mentor Mentor { get; set; } = null!;
    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;
}
