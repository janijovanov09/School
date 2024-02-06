using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolApi.Entities;

[Table("CourseParticipants")]
public class CourseParticipant
{
    [Column(Order = 1)]
    public long CourseId { get; set; }
    [Column(Order = 2)]
    public Guid ParticipantId { get; set; }

    public virtual Course Course { get; set; }
    public virtual Participant Participant { get; set; }
}
