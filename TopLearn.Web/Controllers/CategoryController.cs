using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult GetSubCategories(int id)
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Choose", Value = "" }
            };
            list.AddRange(_categoryService.GetSubCategories(id));
            return Json(new SelectList(list, "Value", "Text"));
        }
    }
}
