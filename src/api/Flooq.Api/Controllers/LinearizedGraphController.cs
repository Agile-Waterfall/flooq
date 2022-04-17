#nullable disable
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

        public LinearizedGraphController(ILinearizedGraphService graphService)
        { 
          _graphService = graphService;
        }

        // GET: api/LinearizedGraph
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LinearizedGraph>>> GetGraphs()
        {
          return await _graphService.GetGraphs();
        }

        // GET: api/LinearizedGraph/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LinearizedGraph>> GetGraph(Guid id)
        {
            var actionResult = await _graphService.GetGraph(id);
            return actionResult.Value == null ? NotFound() : actionResult;
        }

        // POST: api/LinearizedGraph
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LinearizedGraph>> PostGraph(LinearizedGraph linearizedGraph)
        {
            if (LinearizedGraphExists(linearizedGraph.Id))
            {
              return BadRequest();
            }

            _graphService.AddGraph(linearizedGraph);
            await _graphService.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGraph), new { id = linearizedGraph.Id }, linearizedGraph);
        }

        private bool LinearizedGraphExists(Guid id)
        {
          return _graphService.LinearizedGraphExists(id);
        }
    }
}
