using TopLearn.Core.Interfaces;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Core.Services;

public class CourseService : ICourseService
{
    private readonly TopLearnContext _context;

    public CourseService(TopLearnContext context)
    {
        _context = context;
    }

    public List<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }
}