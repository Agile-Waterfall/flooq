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
        private readonly IMetricsService _metricsService;

        public LinearizedGraphController(ILinearizedGraphService graphService, IMetricsService metricsService)
        { 
          _graphService = graphService;
          _metricsService = metricsService;
        }

        // GET: api/LinearizedGraph
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LinearizedGraph>>> GetGraphs()
        {
          _metricsService.IncrementRequestedListsCount();
          return await _graphService.GetGraphs();
        }

        // GET: api/LinearizedGraph/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LinearizedGraph>> GetGraph(Guid id)
        {
            var actionResult = await _graphService.GetGraph(id);

            if (actionResult.Value == null)
            {
              _metricsService.IncrementNotFoundCount();
              return NotFound();
            }

            _metricsService.IncrementRequestedByIdCount();
            return actionResult;
        }

        // POST: api/LinearizedGraph
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LinearizedGraph>> PostGraph(LinearizedGraph linearizedGraph)
        {
            if (LinearizedGraphExists(linearizedGraph.Id))
            {
              _metricsService.IncrementBadRequestCount();
              return BadRequest();
            }

            _graphService.AddGraph(linearizedGraph);
            await _graphService.SaveChangesAsync();

            _metricsService.IncrementCreatedCount();
            return CreatedAtAction(nameof(GetGraph), new { id = linearizedGraph.Id }, linearizedGraph);
        }

        private bool LinearizedGraphExists(Guid id)
        {
          return _graphService.LinearizedGraphExists(id);
        }
    }
}
