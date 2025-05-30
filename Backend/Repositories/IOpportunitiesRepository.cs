using Backend.Models;

namespace Backend.Repositories
{
    public interface IOpportunitiesRepository
    {
        Task<IEnumerable<Opportunities>> GetAllOpportunities();
        Task AddOpportunity(Opportunities opportunity);
        Task<Opportunities> UpdateOpportunity(Opportunities opportunity);
        Task<bool> DeleteOpportunity(int id);
    }
}
