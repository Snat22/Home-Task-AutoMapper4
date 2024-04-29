using Domain.Entities;

namespace Domain.DTOs.StudentDto;

public class StudentAndMentor_Group
{
    public Group Group { get; set; }
    public Student Student { get; set; }
    public Mentor Mentor { get; set; }
}
