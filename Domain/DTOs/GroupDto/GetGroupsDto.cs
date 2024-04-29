using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;
using Domain.Enums;

namespace Domain.DTOs.GroupDto;

public class GetGroupsDto
{
    public int Id { get; set; }
    public string? GroupName { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public int CourseId { get; set; }

}
