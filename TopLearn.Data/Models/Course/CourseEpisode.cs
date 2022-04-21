using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopLearn.Data.Models.Course;

public class CourseEpisode
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }
    public string File { get; set; }
    
    [Required]
    public TimeSpan Duration { get; set; }
    public bool IsFree { get; set; }

    //Relation Fields
    [Required]
    public int CourseId { get; set; }


    #region Relations

    [ForeignKey("CourseId")]
    public Course Course { get; set; }

    #endregion
}