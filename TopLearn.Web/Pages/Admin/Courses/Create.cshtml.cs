using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.Courses;
using TopLearn.Data.Models.Course;

namespace TopLearn.Web.Pages.Admin.Courses
{
    public class CreateModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;

        public CreateModel(ICourseService courseService, ICategoryService categoryService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public CreateCourseViewModel Course { get; set; }

        public void OnGet()
        {
            var categories = _categoryService.GetForAdmin();
            ViewData["Categories"] = new SelectList(categories, "Value", "Text");

            var subCats = _categoryService.GetSubCategories(int.Parse(categories.First().Value));
            ViewData["SubCategories"] = new SelectList(subCats, "Value", "Text");

            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text");

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text");

            var statuses = _courseService.GetStatuses();
            ViewData["Statuses"] = new SelectList(statuses, "Value", "Text");
        }


        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .ToList();
                var categories = _categoryService.GetForAdmin();
                ViewData["Categories"] = new SelectList(categories, "Value", "Text");

                var subCats = _categoryService.GetSubCategories(int.Parse(categories.First().Value));
                ViewData["SubCategories"] = new SelectList(subCats, "Value", "Text");

                var teachers = _courseService.GetTeachers();
                ViewData["Teachers"] = new SelectList(teachers, "Value", "Text");

                var levels = _courseService.GetLevels();
                ViewData["Levels"] = new SelectList(levels, "Value", "Text");

                var statuses = _courseService.GetStatuses();
                ViewData["Statuses"] = new SelectList(statuses, "Value", "Text");
                
                return Page();
            }

            _courseService.Add(new Course()
            {
                Title = Course.Title,
                Description = Course.Description,
                Price = Course.Price,
                Tags = Course.Tags,
                CategoryId = Course.CategoryId,
                SubCategoryId = Course.SubCategoryId,
                TeacherId = Course.TeacherId,
                StatusId = Course.StatusId,
                LevelId = Course.LevelId
            }, imgCourseUp, demoUp);

            return RedirectToPage("Index");
        }
    }
}
