using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Backend.Repositories;

namespace Backend.Controllers
{
    [Authorize]
    public class CodingStatsController : BaseApiController
    {
        public CodingStatsController(
            IRegisterRepository registerRepository,
            IProfileRepository profileRepository,
            ICoursesRepository coursesRepository,
            IOpportunitiesRepository opportunitiesRepository,
            ICodingStatsRepository codingStatsRepository)
            : base(registerRepository, profileRepository, coursesRepository, opportunitiesRepository, codingStatsRepository)
        {
        }

        [HttpPost("update-leetcode")]
        public async Task<IActionResult> UpdateLeetcodeStats([FromBody] string username)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found.");
            }

            await _codingStatsRepository.UpdateLeetcodeStats(username);
            return Ok();
        }

        [HttpPost("update-codeforces")]
        public async Task<IActionResult> UpdateCodeforcesStats([FromBody] string username)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found.");
            }

            await _codingStatsRepository.UpdateCodeforcesStats(username);
            return Ok();
        }

        [HttpPost("update-gfg")]
        public async Task<IActionResult> UpdateGfgStats([FromBody] string username)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found.");
            }

            await _codingStatsRepository.UpdateGfgStats(username);
            return Ok();
        }

        [HttpPost("update-hackerrank")]
        public async Task<IActionResult> UpdateHackerrankStats([FromBody] string username)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found.");
            }

            await _codingStatsRepository.UpdateHackerrankStats(username);
            return Ok();
        }
    }
} 