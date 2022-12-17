using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopLearn.Data.Entities.Course;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "عنوان دسته بندی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد .")]
    public string Title { get; set; }

    public int? ParentId { get; set; }

    #region Relations
    [ForeignKey("ParentId")]
    public List<Category> SubCategories { get; set; }

    #endregion
}