using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Flooq.Api.Metrics.Services;

namespace Flooq.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DataFlowController : ControllerBase
  {
    private readonly IDataFlowService _dataFlowService;
    private readonly ILinearizedGraphService _graphService;
    private readonly IDataFlowMetricsService _dataFlowMetricsService;

    public DataFlowController(IDataFlowService dataFlowService, ILinearizedGraphService graphService, IDataFlowMetricsService dataFlowMetricsService)
    {
      _dataFlowService = dataFlowService;
      _graphService = graphService;
      _dataFlowMetricsService = dataFlowMetricsService;
    }

    // GET: api/DataFlow
    /// <summary>
    /// Gets every <see cref="DataFlow"/> of the current user.
    /// </summary>
    /// <returns>Every <see cref="DataFlow"/> of the current user.</returns>
    [HttpGet("user")]
    [Authorize("read")]
    public async Task<ActionResult<IEnumerable<DataFlow>>> GetDataFlowsByUser()
    {
      _dataFlowMetricsService.IncrementRequestedListsCount();
      return await _dataFlowService.GetDataFlowsByUserId(GetCurrentUserId());
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
    [Authorize("read_all")]
    public async Task<ActionResult<DataFlow?>> GetDataFlow(Guid? id)
    {
      var actionResult = await _dataFlowService.GetDataFlowById(id);
      _dataFlowMetricsService.IncrementRequestedByIdCount();
      return actionResult.Value == null ? NotFound() : actionResult;
    }

    // GET: api/DataFlow/5
    /// <summary>
    /// Gets a specific <see cref="DataFlow"/> of the current user by id.
    /// </summary>
    /// <param name="id">Identifies the specific <see cref="DataFlow"/>.</param>
    /// <returns>
    /// The specific <see cref="DataFlow"/>
    /// or <see cref="NotFoundResult"/> if no <see cref="DataFlow"/> was identified by the id.
    /// </returns>
    [HttpGet("user/{id}")]
    [Authorize("read")]
    public async Task<ActionResult<DataFlow?>> GetDataFlowByUser(Guid? id)
    {
      var actionResult = await _dataFlowService.GetDataFlowByIdByUserId(id, GetCurrentUserId());

      if (actionResult.Value == null)
      {
        _dataFlowMetricsService.IncrementNotFoundCount();
        return NotFound();
      }

      _dataFlowMetricsService.IncrementRequestedByIdCount();
      return actionResult;
    }

    // PUT: api/DataFlow/5
    /// <summary>
    /// Overrides a specific <see cref="DataFlow"/> with a new <see cref="DataFlow"/>.
    /// Parameter id has to match the id of the put <see cref="DataFlow"/>.
    /// Even if not null, the field lastEdited will be ignored. Instead, it's automatically updated.
    /// </summary>
    /// <param name="id">Identifies the specific <see cref="DataFlow"/>. Has to match the id of the new <see cref="DataFlow"/>.</param>
    /// <param name="dataFlow">The new <see cref="DataFlow"/>. Its id has to match the parameter id.</param>
    /// <returns>
    /// The specific <see cref="DataFlow"/>
    /// or <see cref="BadRequestResult"/> if ids of do not match
    /// or <see cref="UnauthorizedResult"/> if user id does not match the user id of the currently saved dataflow
    /// or <see cref="NotFoundResult"/> if no <see cref="DataFlow"/> was identified by the id.
    /// </returns>
    [HttpPut("{id}")]
    [Authorize("write")]
    public async Task<ActionResult<DataFlow>> PutDataFlow(Guid? id, DataFlow dataFlow)
    {
      if (id == null || id != dataFlow.Id)
      {
        _dataFlowMetricsService.IncrementBadRequestCount();
        return BadRequest();
      }

      if (!_dataFlowService.IsDataFlowOwnedByUser(id, dataFlow.UserId))
      {
        return Unauthorized();
      }

      dataFlow.LastEdited = DateTime.UtcNow;

      var actionResultDataFlow = _dataFlowService.PutDataFlow(dataFlow);

      await _dataFlowService.SaveChangesAsync();

      // Delete LinearizedGraph of changed DataFlow
      var actionResultGraph = await _graphService.GetGraph(id.Value);
      var graph = actionResultGraph?.Value; // Conditional access qualifier is needed!
      if (graph != null)
      {
        _graphService.RemoveGraph(graph);
        await _graphService.SaveChangesAsync();
      }

      _dataFlowMetricsService.IncrementEditedCount();
      return actionResultDataFlow;
    }

    // POST: api/DataFlow
    /// <summary>
    /// Adds a <see cref="DataFlow"/>.
    /// If null, the uuid is automatically created and set.
    /// If null, the status is set to "Disabled".
    /// Even if not null, the field lastEdited will be ignored. Instead, it's automatically created.
    /// </summary>
    /// <param name="dataFlow">The new <see cref="DataFlow"/>.</param>
    /// <returns>
    /// A <see cref="CreatedAtActionResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response
    /// or <see cref="ConflictResult"/> if the <see cref="DataFlow"/> already exists.
    /// </returns>
    [HttpPost]
    [Authorize("write")]
    public async Task<ActionResult<DataFlow>> PostDataFlow(DataFlow dataFlow)
    {
      dataFlow.UserId = GetCurrentUserId();
      dataFlow.LastEdited = DateTime.UtcNow;
      
      _dataFlowService.AddDataFlow(dataFlow);
      try
      {
        await _dataFlowService.SaveChangesAsync();
      }
      catch (Exception)
      {
        if (DataFlowExists(dataFlow.Id))
        {
          return Conflict();
        }
        throw;
      }

      _dataFlowMetricsService.IncrementCreatedCount();
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
    [Authorize("write")]
    public async Task<IActionResult> DeleteDataFlow(Guid? id)
    {
      var actionResult = await _dataFlowService.GetDataFlowById(id);
      var dataFlow = actionResult?.Value; // Conditional access qualifier is needed!

      if (dataFlow == null)
      {
        _dataFlowMetricsService.IncrementNotFoundCount();
        return NotFound();
      }

      _dataFlowService.RemoveDataFlow(dataFlow);
      await _dataFlowService.SaveChangesAsync();

      _dataFlowMetricsService.IncrementDeletedCount();
      return NoContent();
    }

    // DELETE: api/DataFlow/all
    /// <summary>
    /// Deletes all <see cref="DataFlow"/> for a user.
    /// </summary>
    /// <returns><see cref="NoContentResult"/> if deletion was successful.</returns>
    [HttpDelete("all")]
    [Authorize("write")]
    public async Task<IActionResult> DeleteAllDataFlows()
    {
      var userId = GetCurrentUserId();
      _dataFlowService.RemoveAllDataFlowsByUserId(userId);
      await _dataFlowService.SaveChangesAsync();

      return NoContent();
    }

    private Guid GetCurrentUserId()
    {
      return Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
    }

    private bool DataFlowExists(Guid? id)
    {
      return _dataFlowService.DataFlowExists(id);
    }
  }
}
