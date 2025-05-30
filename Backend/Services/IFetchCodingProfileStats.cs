using Backend.Models;

namespace Backend.Services
{
    public interface IFetchCodingProfileStats
    {
        public Task<CodingPlatformStats> FetchLeetcodeStats(String username);
        public Task<CodingPlatformStats> FetchCodeForcesStats(String username);
        public Task<CodingPlatformStats> FetchGfgStats(String username);
        public Task<CodingPlatformStats> FetchHackerrankStats(String username);

    }
}
