#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flooq.Api.Domain;
using Flooq.Api.Models;

namespace Flooq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinearizedGraphController : ControllerBase
    {
        private readonly FlooqContext _context;

        public LinearizedGraphController(FlooqContext context)
        {
            _context = context;
        }

        // GET: api/LinearizedGraph
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LinearizedGraph>>> GetGraphs()
        {
            return await _context.Graphs.ToListAsync();
        }

        // GET: api/LinearizedGraph/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LinearizedGraph>> GetLinearizedGraph(Guid id)
        {
            var linearizedGraph = await _context.Graphs.FindAsync(id);

            if (linearizedGraph == null)
            {
                return NotFound();
            }

            return linearizedGraph;
        }

        // POST: api/LinearizedGraph
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LinearizedGraph>> PostLinearizedGraph(LinearizedGraph linearizedGraph)
        {
            if (LinearizedGraphExists(linearizedGraph.Id))
            {
              return BadRequest();
            }
            
            _context.Graphs.Add(linearizedGraph);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLinearizedGraph), new { id = linearizedGraph.Id }, linearizedGraph);
        }

        private bool LinearizedGraphExists(Guid id)
        {
            return _context.Graphs.Any(e => e.Id == id);
        }
    }
}
