#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// Gets every <see cref="Contact"/>.
        /// </summary>
        /// <returns>Every <see cref="Contact"/>.</returns>
        [HttpGet]
        [Authorize("read_all")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
          return await _contactService.GetContacts();
        }

        // GET: api/Contact/{email}
        /// <summary>
        /// Gets a specific <see cref="Contact"/> by email address.
        /// </summary>
        /// <param name="email">Identifies the specific <see cref="Contact"/>.</param>
        /// <returns>
        /// The specific <see cref="Contact"/>
        /// or <see cref="NotFoundResult"/> if no <see cref="Contact"/> was identified by the email address.
        /// </returns>
        [HttpGet("{email}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<Contact>> GetContact(string email)
        {
          var contact = await _contactService.GetContact(email);
          return contact.Value == null ? NotFound() : contact;
        }

        // POST: api/Contact
        /// <summary>
        /// Adds a <see cref="Contact"/>.
        /// </summary>
        /// <param name="contact">The new <see cref="Contact"/>.</param>
        /// <returns>
        /// A <see cref="CreatedAtActionResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response
        /// or <see cref="ConflictResult"/> if the contact already exists.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
          _contactService.AddContact(contact);
          try
          {
            await _contactService.SaveChangesAsync();
          }
          catch (Exception)
          {
            if (ContactExists(contact.Email))
            {
                return Conflict();
            }
            throw;
          }

          return CreatedAtAction(nameof(GetContact),new { email = contact.Email }, contact);
        }

        // DELETE: api/Contact/{email}
        /// <summary>
        /// Deletes a specific <see cref="Contact"/>.
        /// </summary>
        /// <param name="email">Identifies the specific <see cref="Contact"/>.</param>
        /// <returns>
        /// <see cref="NoContentResult"/> if deletion was successful
        /// or <see cref="NotFoundResult"/> if no <see cref="Contact"/> was identified by the email address.
        /// </returns>
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
