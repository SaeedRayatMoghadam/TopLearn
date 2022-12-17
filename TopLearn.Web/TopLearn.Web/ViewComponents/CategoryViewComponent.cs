using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Interfaces;

namespace TopLearn.Web.ViewComponents;

public class CategoryViewComponent:ViewComponent
{
    private readonly ICourseService _courseService;

    public CategoryViewComponent(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult((IViewComponentResult) View("Category", _courseService.GetCategories()));
    }
}