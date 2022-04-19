namespace TopLearn.Core.ViewModels.Categories;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? ParentId { get; set; }
}