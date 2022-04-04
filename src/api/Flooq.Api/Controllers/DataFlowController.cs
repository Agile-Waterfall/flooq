#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flooq.Api.Models;
using Flooq.Api.Services;

namespace Flooq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataFlowController : ControllerBase
    { 
        private readonly IDataFlowService _dataFlowService;

        public DataFlowController(IDataFlowService dataFlowService)
        { 
          _dataFlowService = dataFlowService;
        }

        // GET: api/DataFlow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataFlow>>> GetDataFlows()
        {
          return await _dataFlowService.GetDataFlows();
        }

        // GET: api/DataFlow/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataFlow>> GetDataFlow(Guid id)
        {
          var dataFlow = await _dataFlowService.GetDataFlow(id);

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

            _dataFlowService.SetEntryState(dataFlow, EntityState.Modified);

            try
            {
                await _dataFlowService.SaveChangesAsync();
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
            _dataFlowService.AddDataFlow(dataFlow);
            await _dataFlowService.SaveChangesAsync();

            return CreatedAtAction("GetDataFlow", new { id = dataFlow.Id }, dataFlow);
        }

        // DELETE: api/DataFlow/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataFlow(Guid id)
        {
            var dataFlow = await _dataFlowService.GetDataFlow(id);
            if (dataFlow.Value == null)
            {
                return NotFound();
            }

            _dataFlowService.RemoveDataFlow(dataFlow.Value);
            await _dataFlowService.SaveChangesAsync();

            return NoContent();
        }

        private bool DataFlowExists(Guid id)
        {
            return _dataFlowService.DataFlowExists(id);
        }
    }
}
