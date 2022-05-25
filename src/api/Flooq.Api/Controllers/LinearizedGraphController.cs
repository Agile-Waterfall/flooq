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
        /// <summary>
        /// Gets every <see cref="LinearizedGraph"/>.
        /// </summary>
        /// <returns>Every <see cref="LinearizedGraph"/>.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LinearizedGraph>>> GetGraphs()
        {
          _graphMetricsService.IncrementRequestedListsCount();
          return await _graphService.GetGraphs();
        }

        // GET: api/LinearizedGraph/5
        /// <summary>
        /// Gets a specific <see cref="LinearizedGraph"/> by id.
        /// </summary>
        /// <param name="id">Identifies the specific <see cref="LinearizedGraph"/>.</param>
        /// <returns>
        /// The specific <see cref="LinearizedGraph"/>
        /// or <see cref="NotFoundResult"/> if no <see cref="LinearizedGraph"/> was identified by the id.
        /// </returns>
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
        /// <summary>
        /// Adds a <see cref="LinearizedGraph"/>.
        /// </summary>
        /// <param name="linearizedGraph">The new <see cref="LinearizedGraph"/>.</param>
        /// <returns>
        /// A <see cref="CreatedAtActionResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response
        /// or <see cref="ConflictResult"/> if the <see cref="LinearizedGraph"/> already exists.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<LinearizedGraph>> PostGraph(LinearizedGraph linearizedGraph)
        {
            _graphService.AddGraph(linearizedGraph);
            try
            {
              await _graphService.SaveChangesAsync();
            }
            catch (Exception)
            {
              if (LinearizedGraphExists(linearizedGraph.Id))
              {
                _graphMetricsService.IncrementConflictCount();
                return Conflict();
              }
              _graphMetricsService.IncrementExceptionCount();
              throw;
            }

            _graphMetricsService.IncrementCreatedCount();
            return CreatedAtAction(nameof(GetGraph), new { id = linearizedGraph.Id }, linearizedGraph);
        }

        private bool LinearizedGraphExists(Guid id)
        {
            return _graphService.LinearizedGraphExists(id);
        }
    }
}
