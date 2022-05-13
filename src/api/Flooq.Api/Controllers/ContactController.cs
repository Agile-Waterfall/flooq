#nullable disable
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

        public ContactController(IContactService contactService)
        {
          _contactService = contactService;
        }

        // GET: api/Contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
          return await _contactService.GetContacts();
        }

        // GET: api/Contact/5
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
                return Conflict();
            }
            throw;
          }

          return CreatedAtAction(nameof(GetContact),new { email = contact.Email }, contact);
        }

        // DELETE: api/Contact/5
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteContact(string email)
        {
          var actionResult = await _contactService.GetContact(email);
          var contact = actionResult?.Value;
          
          if (contact == null)
          {
            return NotFound();
          }

          _contactService.RemoveContact(contact);
          await _contactService.SaveChangesAsync();

          return NoContent();
        }

        private bool ContactExists(string email)
        {
          return _contactService.ContactExists(email);
        }
    }
}
