using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopLearn.Data.Models.Categories;

public class Category
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    public int? ParentId { get; set; }
    public bool IsDeleted { get; set; }

    #region Relations

    [ForeignKey("ParentId")]
    public List<Category> Categories { get; set; }

    #endregion
}