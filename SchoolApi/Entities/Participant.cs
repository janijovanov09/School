using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApi.Entities;

[Table("Participant")]
public class Participant
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public virtual ICollection<CourseParticipant> CourseParticipants { get; set; }
}
