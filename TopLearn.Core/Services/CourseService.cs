using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopLearn.Core.Interfaces;
using TopLearn.Core.Utils;
using TopLearn.Core.ViewModels.Courses;
using TopLearn.Data.Context;
using TopLearn.Data.Models.Course;

namespace TopLearn.Core.Services;

public class CourseService : ICourseService
{
    private readonly TopLearnDbContext _context;

    public CourseService(TopLearnDbContext context)
    {
        _context = context;
    }

    public List<CourseViewModel> GetAll()
    {
        return _context.Courses
            .Select(c => new CourseViewModel()
            {
                Id = c.Id,
                Title = c.Title,
                Price = c.Price,
                Image = c.Image,
                EpisodesCount = c.CourseEpisodes.Count,
            }).ToList();
    }

    public List<SelectListItem> GetTeachers()
    {
        return _context.UserRoles
            .Where(r => r.RoleId == 2)                        
            .Select(r => new SelectListItem()
            {
                Value = r.UserId.ToString(),
                Text = r.User.UserName
            }).ToList();
    }

    public List<SelectListItem> GetLevels()
    {
        return _context.CourseLevels.Select(l => new SelectListItem()
        {
            Value = l.Id.ToString(),
            Text = l.Title
        }).ToList();
    }

    public List<SelectListItem> GetStatuses()
    {
        return _context.CourseStatuses.Select(s => new SelectListItem()
        {
            Value = s.Id.ToString(),
            Text = s.Title
        }).ToList();
    }

    public int Add(Course course, IFormFile courseImage, IFormFile courseDemo)
    {
        course.CreateDate = DateTime.Now;
        course.Image = "no-photo.jpg";

        if (courseImage != null && courseImage.IsImage())
        {
            course.Image = Generator.GenerateGuid() + Path.GetExtension(courseImage.FileName);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/images", course.Image);

            using var stream = new FileStream(imagePath, FileMode.Create);
            courseImage.CopyTo(stream);


            var thumb = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumbs", course.Image);
            var isExist = File.Exists(imagePath);
            var pathExist = Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/course/images"));
            ImageResizer.resize(stream, thumb,150);
        }

        if (courseDemo != null)
        {
            course.DemoVideo = Generator.GenerateGuid() + Path.GetExtension(courseDemo.FileName);
            string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demos", course.DemoVideo);
            
            using var stream = new FileStream(demoPath, FileMode.Create);
            courseImage.CopyTo(stream);
        }


        _context.Courses.Add(course);
        _context.SaveChanges();

        return course.Id;
    }
}