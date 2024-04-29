using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;
using Domain.Enums;

namespace Domain.DTOs.GroupDto;

public class AddGroupDto
{
    public required string GroupName { get; set; }
    public required string? Description { get; set; }
    public Status Status { get; set; }
    [ForeignKey("CourseId")]
    public int CourseId { get; set; }
}
