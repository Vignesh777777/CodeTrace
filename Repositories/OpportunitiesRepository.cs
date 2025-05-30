using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class OpportunitiesRepository : IOpportunitiesRepository
    {
        private readonly ApplicationDbContext _context;

        public OpportunitiesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Opportunities>> GetAllOpportunities()
        {
            return await _context.Opportunities.ToListAsync();
        }

        public async Task AddOpportunity(Opportunities opportunity)
        {
            await _context.Opportunities.AddAsync(opportunity);
            await _context.SaveChangesAsync();
        }

        public async Task<Opportunities> UpdateOpportunity(Opportunities opportunity)
        {
            var opportunityToUpdate = await _context.Opportunities.FindAsync(opportunity.Id);
            if (opportunityToUpdate == null)
            {
                throw new KeyNotFoundException($"Opportunity with ID {opportunity.Id} not found");
            }
            opportunityToUpdate.opportunityName = opportunity.opportunityName;
            opportunityToUpdate.opportunityDescription = opportunity.opportunityDescription;
            opportunityToUpdate.opportunityCompany = opportunity.opportunityCompany;
            opportunityToUpdate.opportunityLink = opportunity.opportunityLink;
            opportunityToUpdate.opportunityImageUrl = opportunity.opportunityImageUrl;
            opportunityToUpdate.opportunityDeadline = opportunity.opportunityDeadline;
            _context.Opportunities.Update(opportunityToUpdate);
            await _context.SaveChangesAsync();
            return opportunityToUpdate;
        }

        public async Task<bool> DeleteOpportunity(int id)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);
            if (opportunity == null)
            {
                throw new KeyNotFoundException($"Opportunity with ID {id} not found");
            }
            _context.Opportunities.Remove(opportunity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
