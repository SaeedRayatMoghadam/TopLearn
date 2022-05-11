using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.ViewModels.Courses;
using TopLearn.Data.Models.Course;

namespace TopLearn.Core.Interfaces;

public interface ICourseService
{
    List<SelectListItem> GetTeachers();
    List<SelectListItem> GetLevels();
    List<SelectListItem> GetStatuses();

    List<CourseViewModel> GetAll();
    int Add(Course course, IFormFile courseImage, IFormFile courseDemo);
}