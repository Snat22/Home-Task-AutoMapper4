namespace Domain.DTOs.ProgressBookDto;

public class UpdateProgressBookDto
{
    public int Id { get; set; }
    public int Grade { get; set; }
    public bool IsAttended { get; set; }
    public string Note { get; set; } = null!;
    public int LateMinutes { get; set; }
    public int StudentId { get; set; }
    public int GroupId { get; set; }
    public int TimeTableId { get; set; }
}
