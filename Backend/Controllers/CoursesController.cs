using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    public class CoursesController : BaseApiController
    {
        private readonly ICoursesRepository _coursesRepository;

        public CoursesController(ICoursesRepository coursesRepository) : base(coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        [HttpGet("get-all-courses")]
        public async Task<List<Courses>> GetAllCourses()
        {
            var coursesList = await _coursesRepository.GetAllCoursesAsync();
            return coursesList;
        }
        [HttpPost("add-course")]
        public async Task<IActionResult> AddCourse([FromBody] Courses course)
        {
            if (course == null)
            {
                return BadRequest("Course cannot be null");
            }
            await _coursesRepository.AddCourseAsync(course);
            return Ok();
        }
        [HttpPut("update-course")]
        public async Task<IActionResult> UpdateCourse([FromBody] Courses course)
        {
            if (course == null)
            {
                return BadRequest("Course cannot be null");
            }
            await _coursesRepository.UpdateCourseAsync(course);
            return Ok();
        }
        [HttpDelete("delete-course/{courseId}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            if (courseId <= 0)
            {
                return BadRequest("Invalid course ID");
            }
            await _coursesRepository.DeleteCourseAsync(courseId);
            return Ok();
        }

    }
}
