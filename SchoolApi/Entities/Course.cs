using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SchoolApi.Entities;

[Table("Course")]
public class Course
{
    [Key]
    [JsonPropertyName("id")]
    public long Id { get; set; }
    public string? CompanyName { get; set; }

    [NotMapped]
    [JsonPropertyName("dates")]
    public IEnumerable<DateOnly> Dates {get; set; } = Enumerable.Empty<DateOnly>();

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    public string? CompanyPhoneNumber { get; set; }
    public string? CompanyEmail { get; set; }

    public virtual ICollection<CourseParticipant> CourseParticipants { get; set; }

    public virtual ICollection<CourseSchedule> CourseSchedules { get; set; }
}
