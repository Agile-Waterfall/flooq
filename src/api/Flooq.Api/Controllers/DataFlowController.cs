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
        /// <summary>
        /// Gets every <see cref="DataFlow"/>.
        /// </summary>
        /// <returns>Every <see cref="DataFlow"/></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataFlow>>> GetDataFlows()
        {
          return await _dataFlowService.GetDataFlows();
        }

        // GET: api/DataFlow/5
        /// <summary>
        /// Gets a specific <see cref="DataFlow"/> by id.
        /// </summary>
        /// <param name="id">Identifies the specific <see cref="DataFlow"/>.</param>
        /// <returns>
        /// The specific <see cref="DataFlow"/>
        /// or <see cref="NotFoundResult"/> if no <see cref="DataFlow"/> was identified by the id.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DataFlow>> GetDataFlow(Guid? id)
        {
          var actionResult = await _dataFlowService.GetDataFlow(id);

          return actionResult.Value == null ? NotFound() : actionResult;
        }

        // PUT: api/DataFlow/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Overrides a specific <see cref="DataFlow"/> with a new <see cref="DataFlow"/>.
        /// Parameter id has to match the id of the put <see cref="DataFlow"/>.
        /// Even if not null, the field lastEdited will be ignored. Instead, it's automatically updated.
        /// </summary>
        /// <param name="id">Identifies the specific <see cref="DataFlow"/>. Has to match the id of the new <see cref="DataFlow"/>.</param>
        /// <param name="dataFlow">The new <see cref="DataFlow"/>. Its id has to match the parameter id.</param>
        /// <returns>The specific <see cref="DataFlow"/>
        /// or <see cref="BadRequestResult"/> if ids of do not match
        /// or <see cref="NotFoundResult"/> if no <see cref="DataFlow"/> was identified by the id.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<DataFlow>> PutDataFlow(Guid? id, DataFlow dataFlow)
        {
            if (id != dataFlow.Id)
            {
                return BadRequest();
            }

            dataFlow.LastEdited = DateTime.UtcNow;

            var actionResult = _dataFlowService.PutDataFlow(dataFlow);

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
                throw;
            }
            
            // TODO: Delete LinearizedGraph of changed DataFlow
            // https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

            return actionResult;
        }

        // POST: api/DataFlow
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Adds a <see cref="DataFlow"/>.
        /// If null, the uuid is automatically created and set.
        /// If null, the status is set to "Disabled".
        /// Even if not null, the field lastEdited will be ignored. Instead, it's automatically created.
        /// </summary>
        /// <param name="dataFlow">The new <see cref="DataFlow"/>.</param>
        /// <returns>A <see cref="CreatedAtActionResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.</returns>
        [HttpPost]
        public async Task<ActionResult<DataFlow>> PostDataFlow(DataFlow dataFlow)
        {
          if (DataFlowExists(dataFlow.Id))
          {
            return BadRequest();
          }
          
          _dataFlowService.AddDataFlow(dataFlow);
          await _dataFlowService.SaveChangesAsync();

          return CreatedAtAction(nameof(GetDataFlow), new { id = dataFlow.Id }, dataFlow);
        }
        
        // DELETE: api/DataFlow/5
        /// <summary>
        /// Deletes a specific <see cref="DataFlow"/>.
        /// </summary>
        /// <param name="id">Identifies the specific <see cref="DataFlow"/>.</param>
        /// <returns>
        /// <see cref="NoContentResult"/> if deletion was successful
        /// or <see cref="NotFoundResult"/> if no <see cref="DataFlow"/> was identified by the id.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataFlow(Guid? id)
        {
            var actionResult = await _dataFlowService.GetDataFlow(id);
            var dataFlow = actionResult?.Value; // Conditional access qualifier is needed!
            
            if (dataFlow == null)
            {
              return NotFound();
            }

            _dataFlowService.RemoveDataFlow(dataFlow);
            await _dataFlowService.SaveChangesAsync();

            return NoContent();
        }

        private bool DataFlowExists(Guid? id)
        {
            return _dataFlowService.DataFlowExists(id);
        }
    }
}
