using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.Categories;
using TopLearn.Data.Context;
using TopLearn.Data.Models.Categories;

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

    public List<SelectListItem> GetForAdmin()
    {
        return _context.Categories.Where(c => c.ParentId == null).Select(c => new SelectListItem()
        {
            Text = c.Title,
            Value = c.Id.ToString()
        }).ToList();
    }

    public List<SelectListItem> GetSubCategories(int parentId)
    {
        return _context.Categories
            .Where(c => c.ParentId == parentId)
            .Select(c => new SelectListItem()
            {
                Text = c.Title,
                Value = c.Id.ToString()
            })
            .ToList();
    }
}