using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositories
{
    public interface ICodingStatsRepository
    {
        public Task<CodingPlatformStats> GetLeetcodeStats(string username);
        public Task<CodingPlatformStats> GetCodeforcesStats(string username);
        public Task<CodingPlatformStats> GetGfgStats(string username);
        public Task<CodingPlatformStats> GetHackerrankStats(string username);
        public Task UpdateLeetcodeStats(string username);
        public Task UpdateCodeforcesStats(string username);
        public Task UpdateGfgStats(string username);
        public Task UpdateHackerrankStats(string username);
    }
}
