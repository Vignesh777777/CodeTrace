using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using System.Security.Claims;
using Backend.Models;
using Backend.Repositories;

namespace Backend.Controllers
{
    public class FetchCodingPlatformStatsController : BaseApiController
    {
        private readonly ICodingStatsRepository _codingStatsRepository;
        private readonly ApplicationDbContext _context;

        public FetchCodingPlatformStatsController(CodingStatsRepository codingStatsRepository,ApplicationDbContext context) : base(codingStatsRepository)
        {
            _codingStatsRepository = codingStatsRepository;
            _context = context;
        }

        [HttpGet("leetcode/{username}")]
        public async Task<IActionResult> GetLeetcodeStats(string username)
        {
            var stats = await _codingStatsRepository.GetLeetcodeStats(username);
            return Ok(stats);
        }

        [HttpGet("codeforces/{username}")]
        public async Task<IActionResult> GetCodeforcesStats(String username)
        {
            var stats = await _codingStatsRepository.GetCodeforcesStats(username);
            return Ok(stats);
        }

        [HttpGet("gfg/{username}")]
        public async Task<IActionResult> GetGfgStats(string username)
        {
            var stats = await _codingStatsRepository.GetGfgStats(username);
            return Ok(stats);
        }

        [HttpGet("hackerrank/{username}")]
        public async Task<IActionResult> GetHackerrankStats(string username)
        {
            var stats = await _codingStatsRepository.GetHackerrankStats(username);
            return Ok(stats);
        }

        [HttpGet("sync-profiles")]
        public async Task SyncProfiles(string email)
        {
            var userProfile = _context.UserProfiles.FirstOrDefault(e => e.User.Email == email);
            var leetcodeUsername = userProfile.leetcodeUsername;
            var codeforcesUsername = userProfile.codeforcesUsername;
            var gfgUsername = userProfile.gfgUsername;
            var hackerrankUsername = userProfile.hackerrankUsername;
            _codingStatsRepository.UpdateLeetcodeStats(leetcodeUsername);
            _codingStatsRepository.UpdateCodeforcesStats(codeforcesUsername);
            _codingStatsRepository.UpdateGfgStats(gfgUsername);
            _codingStatsRepository.UpdateHackerrankStats(hackerrankUsername);
        }
    }
}
