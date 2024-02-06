using System.Text.Json.Serialization;

namespace SchoolApi.Entities;

public class CourseSchedule
{
    public long CourseId { get; set; }
    public DateOnly Date {get;set;}

    [JsonIgnore]
    public virtual Course Course { get; set; }
}
