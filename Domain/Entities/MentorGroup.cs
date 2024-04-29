namespace Domain.Entities;

public class MentorGroup : BaseEntity
{
    public int MentorId { get; set; }
    public virtual Mentor? Mentor { get; set; }
    public int GroupId { get; set; }
    public virtual Group? Group { get; set; }

}
