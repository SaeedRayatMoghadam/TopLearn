using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.ViewModels.Categories;
using TopLearn.Data.Models.Categories;

namespace TopLearn.Core.Interfaces;

public interface ICategoryService
{
    List<CategoryViewModel> GetAll();
    List<SelectListItem> GetForAdmin();
    List<SelectListItem> GetSubCategories(int parentId);
}