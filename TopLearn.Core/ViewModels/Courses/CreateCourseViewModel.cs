using System.ComponentModel.DataAnnotations;

namespace TopLearn.Core.ViewModels.Courses;

public class CreateCourseViewModel
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int Price { get; set; }

    [MaxLength(500)]
    public string Tags { get; set; }
    
    //Relation Fields
    [Required]
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    [Required]
    public int TeacherId { get; set; }
    [Required]
    public int StatusId { get; set; }
    [Required]
    public int LevelId { get; set; }
}