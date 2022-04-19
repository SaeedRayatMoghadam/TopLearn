using TopLearn.Core.ViewModels.Categories;

namespace TopLearn.Core.Interfaces;

public interface ICategoryService
{
    List<CategoryViewModel> GetAll();
}