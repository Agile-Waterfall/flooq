#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flooq.Api.Domain;
using Flooq.Api.Models;

namespace Flooq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataFlowController : ControllerBase
    {
        private readonly FlooqContext _context;

        public DataFlowController(FlooqContext context)
        {
            _context = context;
        }

        // GET: api/DataFlow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataFlow>>> GetDataFlows()
        {
            return await _context.DataFlows.ToListAsync();
        }

        // GET: api/DataFlow/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataFlow>> GetDataFlow(Guid id)
        {
            var dataFlow = await _context.DataFlows.FindAsync(id);

            if (dataFlow == null)
            {
                return NotFound();
            }

            return dataFlow;
        }

        // PUT: api/DataFlow/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataFlow(Guid id, DataFlow dataFlow)
        {
            if (id != dataFlow.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataFlow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataFlowExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DataFlow
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataFlow>> PostDataFlow(DataFlow dataFlow)
        {
            _context.DataFlows.Add(dataFlow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataFlow", new { id = dataFlow.Id }, dataFlow);
        }

        // DELETE: api/DataFlow/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataFlow(Guid id)
        {
            var dataFlow = await _context.DataFlows.FindAsync(id);
            if (dataFlow == null)
            {
                return NotFound();
            }

            _context.DataFlows.Remove(dataFlow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DataFlowExists(Guid id)
        {
            return _context.DataFlows.Any(e => e.Id == id);
        }
    }
}
