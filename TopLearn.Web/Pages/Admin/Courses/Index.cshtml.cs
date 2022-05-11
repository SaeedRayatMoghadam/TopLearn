using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.Courses;

namespace TopLearn.Web.Pages.Admin.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseViewModel> CourseViewModels { get; set; }

        public void OnGet()
        {
            CourseViewModels = _courseService.GetAll();
        }
    }
}
