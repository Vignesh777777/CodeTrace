using Backend.Models;

namespace Backend.Repositories
{
    public interface ICoursesRepository
    {
        Task<List<Courses>> GetAllCoursesAsync();
        Task AddCourseAsync(Courses course);
        Task UpdateCourseAsync(Courses course);
        Task DeleteCourseAsync(int courseId);
    }
}
