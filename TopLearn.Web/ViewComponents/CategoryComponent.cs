using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Interfaces;

namespace TopLearn.Web.ViewComponents;

public class CategoryComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public CategoryComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult((IViewComponentResult)View("Categories", _categoryService.GetAll()));
    }
}