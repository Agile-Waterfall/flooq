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
  
  [HttpGet("user")]
  [Authorize("read")]
  public async Task<ActionResult<IEnumerable<string>>> GetTokenNamesByUser()
  {
    return await _tokenService.GetTokenNamesByUserId(GetCurrentUserId());
  }
  
  [HttpGet("{id}")]
  [Authorize("read_all")]
  public async Task<ActionResult<Token?>> GetToken(Guid? id)
  {
    var actionResult = await _tokenService.GetTokenById(id);
    return actionResult.Value == null ? NotFound() : actionResult;
  }
  
  [HttpPut("{id}")]
  [Authorize("write")]
  public async Task<ActionResult<Token>> PutToken(Guid? id, Token token)
  {
    if (id == null || id != token.Id)
    {
      return BadRequest();
    }

    if (!_tokenService.IsTokenOwnedByUser(id, token.Name!))
    {
      return Unauthorized();
    }

    token.LastEdited = DateTime.UtcNow;
    var actionResultDataFlow = _tokenService.PutToken(token);
    await _tokenService.SaveChangesAsync();

    return actionResultDataFlow;
  }
  
  [HttpPost]
  [Authorize("write")]
  public async Task<ActionResult<Token>> PostToken(Token token)
  {
    if (TokenExists(token.Id))
    {
      return BadRequest();
    }
          
    token.UserId = GetCurrentUserId();
    token.LastEdited = DateTime.UtcNow;
          
    _tokenService.AddToken(token);
    await _tokenService.SaveChangesAsync();

    return CreatedAtAction(nameof(PostToken), new { id = token.Id }, token);
  }
  
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
  
  private Guid GetCurrentUserId()
  {
    return Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
  }

  private bool TokenExists(Guid? id)
  {
    return _tokenService.TokenExists(id);
  }
}