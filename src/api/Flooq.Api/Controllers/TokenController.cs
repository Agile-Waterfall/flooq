using System.Security.Claims;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flooq.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
  private readonly ITokenService _tokenService;

  public TokenController(ITokenService tokenService)
  {
    _tokenService = tokenService;
  }
  
  // GET: api/Token/user
  /// <summary>
  /// Gets the ids and names of all <see cref="Token"/>s of the current user.
  /// </summary>
  /// <returns>All <see cref="Token"/> ids and names.</returns>
  [HttpGet("user")]
  [Authorize("read")]
  public async Task<ActionResult<IEnumerable<Dictionary<string, string>>>> GetTokenNamesByUser()
  {
    return await _tokenService.GetTokenIdsAndNamesByUserId(GetCurrentUserId());
  }
  
  // GET: api/Token/5
  /// <summary>
  /// Gets a specific <see cref="Token"/> by id.
  /// </summary>
  /// <param name="id">Identifies the specific <see cref="Token"/>.</param>
  /// <returns>
  /// The specific <see cref="Token"/>
  /// or <see cref="NotFoundResult"/> if no <see cref="Token"/> was identified by the id.
  /// </returns>
  [HttpGet("{id}")]
  [Authorize("read_all")]
  public async Task<ActionResult<Token?>> GetToken(Guid? id)
  {
    var actionResult = await _tokenService.GetTokenById(id);
    return actionResult.Value == null ? NotFound() : actionResult;
  }
  
  // PUT: api/Token/5
  /// <summary>
  /// Overrides a specific <see cref="Token"/> with a new <see cref="Token"/>.
  /// Parameter id has to match the id of the put <see cref="Token"/>.
  /// Even if not null, the field lastEdited will be ignored. Instead, it's automatically updated.
  /// </summary>
  /// <param name="id">Identifies the specific <see cref="Token"/>. Has to match the id of the new <see cref="Token"/>.</param>
  /// <param name="token">The new <see cref="Token"/>. Its id has to match the parameter id.</param>
  /// <returns>The specific <see cref="Token"/>
  /// or <see cref="BadRequestResult"/> if <paramref name="id"/> and id of <see cref="Token"/> do not match
  /// or <see cref="UnauthorizedResult"/> if user id does not match the user id of the currently saved <see cref="Token"/></returns>
  [HttpPut("{id}")]
  [Authorize("write")]
  public async Task<ActionResult<Token>> PutToken(Guid? id, Token token)
  {
    if (id == null || id != token.Id)
    {
      return BadRequest();
    }

    if (!IsTokenOwnedByUser(token.Id, token.UserId))
    {
      return Unauthorized();
    }

    token.LastEdited = DateTime.UtcNow;
    var actionResultDataFlow = _tokenService.PutToken(token);
    await _tokenService.SaveChangesAsync();

    return actionResultDataFlow;
  }
  
  // POST: api/Token
  /// <summary>
  /// Adds a <see cref="Token"/>.
  /// If null, the uuid is automatically created and set.
  /// Even if not null, the field lastEdited will be ignored. Instead, it's automatically created.
  /// </summary>
  /// <param name="token">The new <see cref="Token"/>.</param>
  /// <returns>A <see cref="CreatedAtActionResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
  /// or <see cref="BadRequestResult"/> if <see cref="Token"/> already exists or current user has equally named <see cref="Token"/>.</returns>
  [HttpPost]
  [Authorize("write")]
  public async Task<ActionResult<Token>> PostToken(Token token)
  {
    if (TokenExists(token.Id) || HasUserEquallyNamedToken(token.UserId, token.Name!))
    {
      return BadRequest();
    }
          
    token.UserId = GetCurrentUserId();
    token.LastEdited = DateTime.UtcNow;
          
    _tokenService.AddToken(token);
    await _tokenService.SaveChangesAsync();

    return CreatedAtAction(nameof(PostToken), new { id = token.Id }, token);
  }
  
  // DELETE: api/Token/5
  /// <summary>
  /// Deletes a specific <see cref="Token"/>.
  /// </summary>
  /// <param name="id">Identifies the specific <see cref="Token"/>.</param>
  /// <returns>
  /// <see cref="NoContentResult"/> if deletion was successful
  /// or <see cref="NotFoundResult"/> if no <see cref="Token"/> was identified by the id.
  /// </returns>
  [HttpDelete("{id}")]
  [Authorize("write")]
  public async Task<IActionResult> DeleteToken(Guid? id)
  {
    var actionResult = await _tokenService.GetTokenById(id);
    var token = actionResult?.Value; // Conditional access qualifier is needed!
            
    if (token == null)
    {
      return NotFound();
    }

    _tokenService.RemoveToken(token);
    await _tokenService.SaveChangesAsync();

    return NoContent();
  }

  // DELETE: api/Token/all
  /// <summary>
  /// Deletes all <see cref="Token"/> for a user.
  /// </summary>
  /// <returns>
  /// <see cref="NoContentResult"/> if deletion was successful.
  /// </returns>
  [HttpDelete("all")]
  [Authorize("write")]
  public async Task<IActionResult> DeleteAllTokens()
  {
    var userId = GetCurrentUserId();
    _tokenService.RemoveAllTokensByUserId(userId);
    await _tokenService.SaveChangesAsync();

    return NoContent();
  }
  
  private Guid GetCurrentUserId()
  {
    return Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
  }

  private bool TokenExists(Guid? id)
  {
    return _tokenService.TokenExists(id);
  }

  private bool IsTokenOwnedByUser(Guid? id, Guid? userId)
  {
    return _tokenService.IsTokenOwnedByUser(id, userId);
  }

  private bool HasUserEquallyNamedToken(Guid? userId, string name)
  {
    return _tokenService.HasUserEquallyNamedToken(userId, name);
  }
}