using Microsoft.AspNetCore.Mvc;
using Backend.Repositories;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Backend.Controllers;

namespace Backend.Controllers
{
    [Authorize]
    public class OpportunitiesController : BaseApiController
    {
        private readonly IOpportunitiesRepository _opportunitiesRepository;

        public OpportunitiesController(IOpportunitiesRepository opportunitiesRepository) : base(opportunitiesRepository)
        {
            _opportunitiesRepository = opportunitiesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOpportunities()
        {
            var opportunities = await _opportunitiesRepository.GetAllOpportunities();
            return Ok(opportunities);
        }

        [HttpPost("add-opportunity")]
        public async Task<IActionResult> AddOpportunity([FromBody] Opportunities opportunity)
        {
            await _opportunitiesRepository.AddOpportunity(opportunity);
            return Ok(opportunity);
        }

        [HttpPut("update-opportunity")]
        public async Task<IActionResult> UpdateOpportunity([FromQuery] int id, [FromBody] Opportunities opportunity)
        {
            try
            {
                var updatedOpportunity = await _opportunitiesRepository.UpdateOpportunity(opportunity);
                return Ok(updatedOpportunity);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete-opportunity")]
        public async Task<IActionResult> DeleteOpportunity([FromQuery] int id)
        {
            try
            {
                var result = await _opportunitiesRepository.DeleteOpportunity(id);
                if (result)
                {
                    return Ok($"Opportunity with ID {id} deleted successfully");
                }
                return BadRequest($"Failed to delete opportunity with ID {id}");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

