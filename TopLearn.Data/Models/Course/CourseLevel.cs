using System.ComponentModel.DataAnnotations;

namespace TopLearn.Data.Models.Course;

public class CourseLevel
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Title { get; set; }

    
    #region Relations

    public List<Course> Courses { get; set; }

    #endregion
}