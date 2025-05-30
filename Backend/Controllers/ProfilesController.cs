using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace Backend.Controllers
{
    [Authorize]
    public class ProfilesController : BaseApiController
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ApplicationDbContext _context;

        public ProfilesController(IProfileRepository profileRepository, ApplicationDbContext context) : base(profileRepository)
        {
            _profileRepository = profileRepository;
            _context = context;
        }

        [HttpGet("is-Profile-Complete")]
        public async Task<IActionResult> checkIsProfileComplete()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found.");
            }

            var isProfileComplete = await _profileRepository.checkIsProfileComplete(userEmail);
            return Ok(isProfileComplete);
        }

        [HttpGet("get-Profile")]
        public async Task<IActionResult> getProfile()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found.");
            }

            var user = await _profileRepository.getProfile(userEmail);

            if (user == null)
            {
                return NotFound("User profile not found.");
            }

            return Ok(user);
        }

        [HttpPut("update-Profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] User updatedUser)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found.");
            }

            if (updatedUser == null)
            {
                return BadRequest("Invalid profile data.");
            }

            await _profileRepository.UpdateProfileAsync(userEmail, updatedUser);
            return Ok();
        }
        // [HttpGet("get-leetcode-username")]
        // public async Task<IActionResult> getLeetcodeUsername()
        // {
        //     var userEmail = User.FindFirstValue(ClaimTypes.Email);
        //     if (string.IsNullOrEmpty(userEmail))
        //     {
        //         return BadRequest("User email not found.");
        //     }
        //     var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        //     if (user == null)
        //     {
        //         return NotFound("User not found.");
        //     }

        //     var leetcodeProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == user.Id);
            
        //     return Ok(leetcodeProfile?.leetcodeUsername ?? "");
        // }
        // [HttpGet("get-codeforces-username")]
        // public async Task<IActionResult> getCodeforcesUsername()
        // {
        //     var userEmail = User.FindFirstValue(ClaimTypes.Email);
        //     if (string.IsNullOrEmpty(userEmail))
        //     {
        //         return BadRequest("User email not found.");
        //     }
        //     var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        //     if (user == null)
        //     {
        //         return NotFound("User not found.");
        //     }

        //     var codeforcesProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == user.Id);
        //     return Ok(codeforcesProfile?.codeforcesUsername ?? "");
        // }
        // [HttpGet("get-gfg-username")]
        // public async Task<IActionResult> getGfgUsername()
        // {
        //     var userEmail = User.FindFirstValue(ClaimTypes.Email);
        //     if (string.IsNullOrEmpty(userEmail))
        //     {
        //         return BadRequest("User email not found.");
        //     }
        //     var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        //     if (user == null)
        //     {
        //         return NotFound("User not found.");
        //     }

        //     var gfgProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == user.Id);
        //     return Ok(gfgProfile?.gfgUsername ?? "");
        // }
        // [HttpGet("get-hackerrank-username")]
        // public async Task<IActionResult> getHackerrankUsername()
        // {
        //     var userEmail = User.FindFirstValue(ClaimTypes.Email);
        //     if (string.IsNullOrEmpty(userEmail))
        //     {
        //         return BadRequest("User email not found.");
        //     }
        //     var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        //     if (user == null)
        //     {
        //         return NotFound("User not found.");
        //     }

        //     var hackerrankProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == user.Id);
        //     return Ok(hackerrankProfile?.hackerrankUsername ?? "");
        // }
        // [HttpGet("get-codechef-username")]
        // public async Task<IActionResult> getCodechefUsername()
        // {
        //     var userEmail = User.FindFirstValue(ClaimTypes.Email);
        //     if (string.IsNullOrEmpty(userEmail))
        //     {
        //         return BadRequest("User email not found.");
        //     }
        //     var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        //     if (user == null)
        //     {
        //         return NotFound("User not found.");
        //     }

        //     var codechefProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == user.Id);
        //     return Ok(codechefProfile?.codechefUsername ?? "");
        // }
        // [HttpGet("get-github-username")]
        // public async Task<IActionResult> getGithubUsername()
        // {
        //     var userEmail = User.FindFirstValue(ClaimTypes.Email);
        //     if (string.IsNullOrEmpty(userEmail))
        //     {
        //         return BadRequest("User email not found.");
        //     }
        //     var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        //     if (user == null)
        //     {
        //         return NotFound("User not found.");
        //     }

        //     var githubProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == user.Id);
        //     return Ok(githubProfile?.githubUsername ?? "");
        // }
    }
}
