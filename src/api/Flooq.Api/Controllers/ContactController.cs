#nullable disable
using Flooq.Api.Metrics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flooq.Api.Models;
using Flooq.Api.Services;

namespace Flooq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IContactMetricsService _contactMetricsService;

        public ContactController(IContactService contactService, IContactMetricsService contactMetricsService)
        {
          _contactService = contactService;
          _contactMetricsService = contactMetricsService;
        }

        // GET: api/Contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
          _contactMetricsService.IncrementRequestedListsCount();
          return await _contactService.GetContacts();
        }

        // GET: api/Contact/{email}
        [HttpGet("{email}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<Contact>> GetContact(string email)
        {
          var contact = await _contactService.GetContact(email);
          return contact.Value == null ? NotFound() : contact;
        }

        // POST: api/Contact
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
          _contactService.AddContact(contact);
          try
          {
            await _contactService.SaveChangesAsync();
          }
          catch (DbUpdateException)
          {
            if (ContactExists(contact.Email))
            {
              _contactMetricsService.IncrementConflictCount(); 
              return Conflict();
            }
            _contactMetricsService.IncrementExceptionCount();
            throw;
          }

          _contactMetricsService.IncrementCreatedCount();
          return CreatedAtAction(nameof(GetContact),new { email = contact.Email }, contact);
        }

        // DELETE: api/Contact/{email}
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteContact(string email)
        {
          var actionResult = await _contactService.GetContact(email);
          var contact = actionResult?.Value;
          
          if (contact == null)
          {
            _contactMetricsService.IncrementNotFoundCount();
            return NotFound();
          }

          _contactService.RemoveContact(contact);
          await _contactService.SaveChangesAsync();

          _contactMetricsService.IncrementDeletedCount();
          return NoContent();
        }

        private bool ContactExists(string email)
        {
          return _contactService.ContactExists(email);
        }
    }
}
