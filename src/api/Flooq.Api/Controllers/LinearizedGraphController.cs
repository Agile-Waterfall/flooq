#nullable disable
using Flooq.Api.Metrics.Services;
using Microsoft.AspNetCore.Mvc;
using Flooq.Api.Models;
using Flooq.Api.Services;

namespace Flooq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinearizedGraphController : ControllerBase
    {
        private readonly ILinearizedGraphService _graphService;
        private readonly ILinearizedGraphMetricsService _graphMetricsService;

        public LinearizedGraphController(ILinearizedGraphService graphService, ILinearizedGraphMetricsService graphMetricsService)
        { 
          _graphService = graphService;
          _graphMetricsService = graphMetricsService;
        }

        // GET: api/LinearizedGraph
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LinearizedGraph>>> GetGraphs()
        {
          _graphMetricsService.IncrementRequestedListsCount();
          return await _graphService.GetGraphs();
        }

        // GET: api/LinearizedGraph/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LinearizedGraph>> GetGraph(Guid id)
        {
            var actionResult = await _graphService.GetGraph(id);

            if (actionResult.Value == null)
            {
              _graphMetricsService.IncrementNotFoundCount();
              return NotFound();
            }

            _graphMetricsService.IncrementRequestedByIdCount();
            return actionResult;
        }

        // POST: api/LinearizedGraph
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LinearizedGraph>> PostGraph(LinearizedGraph linearizedGraph)
        {
            if (LinearizedGraphExists(linearizedGraph.Id))
            {
              _graphMetricsService.IncrementBadRequestCount();
              return BadRequest();
            }

            _graphService.AddGraph(linearizedGraph);
            await _graphService.SaveChangesAsync();

            _graphMetricsService.IncrementCreatedCount();
            return CreatedAtAction(nameof(GetGraph), new { id = linearizedGraph.Id }, linearizedGraph);
        }

        private bool LinearizedGraphExists(Guid id)
        {
          return _graphService.LinearizedGraphExists(id);
        }
    }
}
