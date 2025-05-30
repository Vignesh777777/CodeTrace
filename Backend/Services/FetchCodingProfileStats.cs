using Backend.Models;
using Newtonsoft.Json.Linq;

namespace Backend.Services
{
    public class FetchCodingProfileStats : IFetchCodingProfileStats
    {
        private readonly HttpClient _httpClient;
        public FetchCodingProfileStats(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CodingPlatformStats> FetchLeetcodeStats(String username)
        {
            var response = await _httpClient.GetStringAsync($"https://leetcode-stats-api.herokuapp.com/${username}");
            var json = JObject.Parse(response);
            if ((string?)json["status"] == "error")
            {
                throw new Exception($"LeetCode username '{username}' not found.");
            }
            var leetcodeDetails = new CodingPlatformStats
            {
                Platform = "leetcode",
                platformUsername = username,
                EasySolved = (int?)json["easySolved"] ?? 0,
                MediumSolved = (int?)json["mediumSolved"] ?? 0,
                HardSolved = (int?)json["hardSolved"] ?? 0,
                TotalSolved = (int?)json["totalSolved"] ?? 0,
                Ranking = (long?)json["ranking"] ?? 0
            };
            return leetcodeDetails;
        }

        public async Task<CodingPlatformStats> FetchCodeForcesStats(String username)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://codeforces.com/api/user.info?handles={username}");
                var json = JObject.Parse(response);
                if ((string?)json["status"] == "FAILED")
                {
                    throw new Exception($"CodeForces username '{username}' not found.");
                }
                var result = json["result"][0];
                var codeforcesDetails = new CodingPlatformStats
                {
                    Platform = "codeforces",
                    platformUsername = username,
                    Rating = (int?)result["rating"] ?? 0,
                    Rank = (String?)result["rank"] ?? "",
                    MaxRating = (int?)result["maxRating"] ?? 0
                };
                return codeforcesDetails;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching CodeForces stats: {ex.Message}");
            }
        }

        public async Task<CodingPlatformStats> FetchGfgStats(String username)
        {
            var response = await _httpClient.GetStringAsync($"https://geeks-for-geeks-api.vercel.app/${username}");
            var json = JObject.Parse(response);
            var info = json["info"];
            var solvedstats = json["solvedStats"];
            var easy = json["solvedStats"] ?? 0;
            var medium = json["solvedStats"] ?? 0;
            var hard = json["solvedStats"] ?? 0;
            if ((string?)json["error"] == "Profile Not Found")
            {
                throw new Exception($"Gfg username '{username}' not found.");
            }
            var gfgDetails = new CodingPlatformStats
            {
                Platform = "geeksforgeeks",
                platformUsername = username,
                Score = (int?)info["codingScore"] ?? 0,
                EasySolved = (int?)easy["easy"] ?? 0,
                MediumSolved = (int?)medium["medium"] ?? 0,
                HardSolved = (int?)hard["hard"] ?? 0,
                TotalSolved = (int?)info["totalProblemsSolved"] ?? 0,
            };
            return gfgDetails;
        }

        public async Task<CodingPlatformStats> FetchHackerrankStats(String username)
        {
            var response = await _httpClient.GetStringAsync($"https://www.hackerrank.com/rest/hackers/${username}/badges");
            var json = JObject.Parse(response);
            if ((string?)json["status"] != "true")
            {
                throw new Exception($"Hackkerank username '{username}' not found.");
            }
            int solved = 0;
            for(int i = 0; i < json["models"].Count(); i++)
            {
                var badge = json["model"]["badges"][i];
                if ((string?)badge["badge_name"] != null)
                {
                    solved += (int?)json["solved"] ?? 0;
                }
            }
            return new CodingPlatformStats
            {
                TotalSolved = solved
            };
            
        }
    }
}
