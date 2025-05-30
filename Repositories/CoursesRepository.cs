using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly ApplicationDbContext _context;
        
        public CoursesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task AddCourseAsync(Courses course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with ID {courseId} not found");
            }
            
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Courses>> GetAllCoursesAsync()
        {
            var coursesList = await _context.Courses.ToListAsync();
            return coursesList;
        }

        public async Task UpdateCourseAsync(Courses course)
        {
            var courseToUpdate = await _context.Courses.FindAsync(course.Id);
            if (courseToUpdate == null)
            {
                throw new KeyNotFoundException($"Course with ID {course.Id} not found");
            }
            
            courseToUpdate.courseName = course.courseName;
            courseToUpdate.courseDescription = course.courseDescription;
            courseToUpdate.courseImageUrl = course.courseImageUrl;
            courseToUpdate.courseDuration = course.courseDuration;
            courseToUpdate.courseLevel = course.courseLevel;
            courseToUpdate.courseLink = course.courseLink;
            
            _context.Courses.Update(courseToUpdate);
            await _context.SaveChangesAsync();
        }
    }
}
