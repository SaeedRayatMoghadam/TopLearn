using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.Categories;
using TopLearn.Data.Context;

namespace TopLearn.Core.Services;

public class CategoryService : ICategoryService
{
    private readonly TopLearnDbContext _context;

    public CategoryService(TopLearnDbContext context)
    {
        _context = context;
    }

    public List<CategoryViewModel> GetAll()
    {
        return _context.Categories.Select(c => new CategoryViewModel()
        {
            Id = c.Id,
            Title = c.Title,
            ParentId = c.ParentId
        }).ToList();
    }
}