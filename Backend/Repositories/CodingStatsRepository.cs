using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class CodingStatsRepository : ICodingStatsRepository
    {
        private readonly IFetchCodingProfileStats _fetchCodingProfileStats;
        private readonly ApplicationDbContext _context;
        public CodingStatsRepository(IFetchCodingProfileStats fetchCodingProfileStats, ApplicationDbContext context)
        {
            _fetchCodingProfileStats = fetchCodingProfileStats;
            _context = context;
        }
        public async Task<CodingPlatformStats> GetLeetcodeStats(string username)
        {
            var entry = _context.CodingPlatformStats.FirstOrDefault(e => e.platformUsername == username && e.Platform == "leetcode");
            if (entry == null)
            {
                return null;
            }
            var leetcodeStats = new CodingPlatformStats
            {
                Platform = entry.Platform,
                platformUsername = entry.platformUsername,
                EasySolved = entry.EasySolved,
                MediumSolved = entry.MediumSolved,
                HardSolved = entry.HardSolved,
                TotalSolved = entry.TotalSolved,
                Ranking = entry.Ranking
            };
            return leetcodeStats;
        }
        public async Task<CodingPlatformStats> GetCodeforcesStats(string username)
        {
            var entry = _context.CodingPlatformStats.FirstOrDefault(e => e.platformUsername == username && e.Platform == "codeforces");
            if (entry == null)
            {
                return null;
            }
            var codeforcesStats = new CodingPlatformStats
            {
                Platform = entry.Platform,
                platformUsername = entry.platformUsername,
                Rating = entry.Rating,
                Rank = entry.Rank,
                MaxRating = entry.MaxRating
            };
            return codeforcesStats;
        }
        public async Task<CodingPlatformStats> GetGfgStats(string username)
        {
            var entry = _context.CodingPlatformStats.FirstOrDefault(e => e.platformUsername == username && e.Platform == "geeksforgeeks");
            if (entry == null)
            {
                return null;
            }
            var gfgStats = new CodingPlatformStats
            {
                Platform = entry.Platform,
                platformUsername = entry.platformUsername,
                EasySolved = entry.EasySolved,
                MediumSolved = entry.MediumSolved,
                HardSolved = entry.HardSolved,
                TotalSolved = entry.TotalSolved,
                Ranking = entry.Ranking
            };
            return gfgStats;
        }
        public async Task<CodingPlatformStats> GetHackerrankStats(string username)
        {
            var entry = _context.CodingPlatformStats.FirstOrDefault(e => e.platformUsername == username && e.Platform == "hackerrank");
            if (entry == null)
            {
                return null;
            }
            var hackerrankStats = new CodingPlatformStats
            {
                Platform = entry.Platform,
                platformUsername = entry.platformUsername,
                TotalSolved = entry.TotalSolved
            };
            return hackerrankStats;
        }
        public async Task UpdateLeetcodeStats(string username)
        {
            try
            {
                var userProfile = await _context.UserProfiles
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.leetcodeUsername == username);
                
                if (userProfile == null)
                {
                    throw new Exception($"No user profile found with LeetCode username: {username}");
                }

                CodingPlatformStats leetcodestats = await _fetchCodingProfileStats.FetchLeetcodeStats(username);
                if (leetcodestats != null)
                {
                    var entry = new CodingPlatformStats
                    {
                        Platform = leetcodestats.Platform,
                        platformUsername = leetcodestats.platformUsername,
                        email = userProfile.User.Email,
                        UserId = userProfile.UserId,
                        UserProfileId = userProfile.Id,
                        EasySolved = leetcodestats.EasySolved,
                        MediumSolved = leetcodestats.MediumSolved,
                        HardSolved = leetcodestats.HardSolved,
                        TotalSolved = leetcodestats.TotalSolved,
                        Ranking = leetcodestats.Ranking
                    };
                    var existingEntry = await _context.CodingPlatformStats
                        .FirstOrDefaultAsync(e => e.platformUsername == username && e.Platform == "leetcode");
                    if (existingEntry != null)
                    {
                        existingEntry.EasySolved = entry.EasySolved;
                        existingEntry.MediumSolved = entry.MediumSolved;
                        existingEntry.HardSolved = entry.HardSolved;
                        existingEntry.TotalSolved = entry.TotalSolved;
                        existingEntry.Ranking = entry.Ranking;
                        _context.CodingPlatformStats.Update(existingEntry);
                    }
                    else
                    {
                        _context.CodingPlatformStats.Add(entry);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateLeetcodeStats for username {username}: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw new Exception($"Error updating LeetCode stats: {ex.Message}");
            }
        }
        public async Task UpdateCodeforcesStats(string username)
        {
            try
            {
                var userProfile = await _context.UserProfiles
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.codeforcesUsername == username);
                
                if (userProfile == null)
                {
                    throw new Exception($"No user profile found with CodeForces username: {username}");
                }

                CodingPlatformStats codeforcesstats = await _fetchCodingProfileStats.FetchCodeForcesStats(username);
                if (codeforcesstats != null)
                {
                    var entry = new CodingPlatformStats
                    {
                        Platform = codeforcesstats.Platform,
                        platformUsername = codeforcesstats.platformUsername,
                        email = userProfile.User.Email,
                        UserId = userProfile.UserId,
                        UserProfileId = userProfile.Id,
                        Rating = codeforcesstats.Rating,
                        Rank = codeforcesstats.Rank,
                        MaxRating = codeforcesstats.MaxRating
                    };
                    var existingEntry = await _context.CodingPlatformStats
                        .FirstOrDefaultAsync(e => e.platformUsername == username && e.Platform == "codeforces");
                    if (existingEntry != null)
                    {
                        existingEntry.Rating = entry.Rating;
                        existingEntry.Rank = entry.Rank;
                        existingEntry.MaxRating = entry.MaxRating;
                        _context.CodingPlatformStats.Update(existingEntry);
                    }
                    else
                    {
                        _context.CodingPlatformStats.Add(entry);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateCodeforcesStats for username {username}: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw new Exception($"Error updating CodeForces stats: {ex.Message}");
            }
        }
        public async Task UpdateGfgStats(string username)
        {
            try
            {
                var userProfile = await _context.UserProfiles
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.gfgUsername == username);
                
                if (userProfile == null)
                {
                    throw new Exception($"No user profile found with GFG username: {username}");
                }

                CodingPlatformStats gfgstats = await _fetchCodingProfileStats.FetchGfgStats(username);
                if (gfgstats != null)
                {
                    var entry = new CodingPlatformStats
                    {
                        Platform = gfgstats.Platform,
                        platformUsername = gfgstats.platformUsername,
                        email = userProfile.User.Email,
                        UserId = userProfile.UserId,
                        UserProfileId = userProfile.Id,
                        EasySolved = gfgstats.EasySolved,
                        MediumSolved = gfgstats.MediumSolved,
                        HardSolved = gfgstats.HardSolved,
                        TotalSolved = gfgstats.TotalSolved,
                        Score = gfgstats.Score
                    };
                    var existingEntry = await _context.CodingPlatformStats
                        .FirstOrDefaultAsync(e => e.platformUsername == username && e.Platform == "geeksforgeeks");
                    if (existingEntry != null)
                    {
                        existingEntry.EasySolved = entry.EasySolved;
                        existingEntry.MediumSolved = entry.MediumSolved;
                        existingEntry.HardSolved = entry.HardSolved;
                        existingEntry.TotalSolved = entry.TotalSolved;
                        existingEntry.Score = entry.Score;
                        _context.CodingPlatformStats.Update(existingEntry);
                    }
                    else
                    {
                        _context.CodingPlatformStats.Add(entry);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateGfgStats for username {username}: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw new Exception($"Error updating GFG stats: {ex.Message}");
            }
        }
        public async Task UpdateHackerrankStats(string username)
        {
            try
            {
                var userProfile = await _context.UserProfiles
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.hackerrankUsername == username);
                
                if (userProfile == null)
                {
                    throw new Exception($"No user profile found with HackerRank username: {username}");
                }

                CodingPlatformStats hackerrankstats = await _fetchCodingProfileStats.FetchHackerrankStats(username);
                if (hackerrankstats != null)
                {
                    var entry = new CodingPlatformStats
                    {
                        Platform = "hackerrank",
                        platformUsername = username,
                        email = userProfile.User.Email,
                        UserId = userProfile.UserId,
                        UserProfileId = userProfile.Id,
                        TotalSolved = hackerrankstats.TotalSolved
                    };
                    var existingEntry = await _context.CodingPlatformStats
                        .FirstOrDefaultAsync(e => e.platformUsername == username && e.Platform == "hackerrank");
                    if (existingEntry != null)
                    {
                        existingEntry.TotalSolved = entry.TotalSolved;
                        _context.CodingPlatformStats.Update(existingEntry);
                    }
                    else
                    {
                        _context.CodingPlatformStats.Add(entry);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateHackerrankStats for username {username}: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw new Exception($"Error updating HackerRank stats: {ex.Message}");
            }
        }
    }
}
