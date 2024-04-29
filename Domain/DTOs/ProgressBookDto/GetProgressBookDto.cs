namespace Domain.DTOs.ProgressBookDto;

public class GetProgressBookDto
{
    public int Id { get; set; }
    public int Grade { get; set; }
    public bool IsAttended { get; set; }
    public string Note { get; set; } = null!;
    public int LateMinutes { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public int StudentId { get; set; }
    public int GroupId { get; set; }
    public int TimeTableId { get; set; }
}
