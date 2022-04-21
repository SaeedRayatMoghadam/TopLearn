using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopLearn.Data.Models.Categories;
using TopLearn.Data.Models.Users;

namespace TopLearn.Data.Models.Course;

public class Course
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int Price { get; set; }

    [MaxLength(500)]
    public string Tags { get; set; }

    [MaxLength(100)]
    public string Image { get; set; }

    [MaxLength(100)]
    public string DemoVideo { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }

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


    #region Relations

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    [ForeignKey("SubCategoryId")]
    public Category SubCategory { get; set; }

    [ForeignKey("TeacherId")] 
    public User User { get; set; }

    [ForeignKey("StatusId")] 
    public CourseStatus CourseStatus { get; set; }
    
    [ForeignKey("LevelId")] 
    public CourseLevel CourseLevel { get; set; }
    

    public List<CourseEpisode> CourseEpisodes { get; set; }

    #endregion
}