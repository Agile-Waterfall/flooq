using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

/// <summary>
/// Provides for querying Flooq <see cref="Contact"/>s from the Flooq database.
/// </summary>
public interface IContactService
{
  /// <summary>
  /// Queries all <see cref="Contact"/>s from the Flooq database.
  /// </summary>
  /// <returns>All <see cref="Contact"/>s from the Flooq database.</returns>
  Task<ActionResult<IEnumerable<Contact>>> GetContacts();

  /// <summary>
  /// Finds a specific <see cref="Contact"/> in the Flooq database according to a unique email.
  /// </summary>
  /// <param name="email">The email which is used for identifying the <see cref="Contact"/>.</param>
  /// <returns>The identified <see cref="Contact"/>.</returns>
  Task<ActionResult<Contact?>>GetContact(string email);

  /// <summary>
  /// Saves all previously made changes in this DbContext to the database.
  /// </summary>
  /// <returns>
  /// A <see cref="Task"/> that represents the asynchronous save operation.
  /// The <see cref="Task"/> result contains the number of state entries written to the database.
  /// </returns>
  Task<int> SaveChangesAsync();

  /// <summary>
  /// Begins tracking the given <see cref="Contact"/>, and any other reachable entities that are not already being tracked,
  /// in the Added state such that they will be inserted into the database when <code>SaveChanges()</code> is called.
  /// </summary>
  /// <param name="contact">The <see cref="Contact"/> to add.</param>
  /// <returns>
  /// The <see cref="EntityEntry"/>.
  /// The entry provides access to change tracking information and operations for the entity.
  /// </returns>
  EntityEntry<Contact> AddContact(Contact contact);

  /// <summary>
  /// Begins tracking the given <see cref="Contact"/> in the Deleted state such that it
  /// will be removed from the database when <code>SaveChanges()</code> is called.
  /// </summary>
  /// <param name="contact">The <see cref="Contact"/> to remove.</param>
  /// <returns>
  /// The <see cref="EntityEntry{TEntity}"/>.
  /// The entry provides access to change tracking information and operations for the entity.
  /// </returns>
  EntityEntry<Contact> RemoveContact(Contact contact);

  /// <summary>
  /// Checks if the <see cref="Contact"/> with the given email exists.
  /// </summary>
  /// <param name="email">The email of the <see cref="Contact"/>.</param>
  /// <returns>true if the <see cref="Contact"/> exists, else false.</returns>
  bool ContactExists(string email);
}