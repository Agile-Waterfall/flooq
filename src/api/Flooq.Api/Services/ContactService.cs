using Flooq.Api.Domain;
using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

public class ContactService : IContactService
{
  private readonly FlooqContext _context;
  
  public ContactService(FlooqContext context)
  {
    _context = context;
  }
  
  public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
  {
    return await _context.Contacts.ToListAsync();
  }

  public async Task<ActionResult<Contact?>> GetContact(string email)
  {
    var contact = await _context.Contacts.FindAsync(email);
    return new ActionResult<Contact?>(contact);
  }

  public async Task<int> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync();
  }

  public EntityEntry<Contact> AddContact(Contact contact)
  {
    return _context.Contacts.Add(contact);
  }

  public EntityEntry<Contact> RemoveContact(Contact contact)
  {
    return _context.Contacts.Remove(contact);
  }

  public bool ContactExists(string email)
  {
    return _context.Contacts.Any(e => e.Email == email);
  }
}