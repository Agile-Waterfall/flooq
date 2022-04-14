using System.Threading.Tasks;
using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

public interface ILinearizedGraphService
{
  /// <summary>
  /// Queries all <see cref="LinearizedGraph"/>s from the Flooq database.
  /// </summary>
  /// <returns>All <see cref="LinearizedGraph"/>s from the Flooq database.</returns>
  Task<ActionResult<IEnumerable<LinearizedGraph>>> GetGraphs();

  /// <summary>
  /// Finds a specific <see cref="LinearizedGraph"/> in the Flooq database according to a unique identification.
  /// </summary>
  /// <returns>The identified <see cref="LinearizedGraph"/>.</returns>
  /// <param name="id">The value which is used for identifying the <see cref="LinearizedGraph"/>.</param>
  Task<ActionResult<LinearizedGraph>> GetGraph(Guid id);

  /// <summary>
  /// Saves all previously made changes in this DbContext to the database.
  /// </summary>
  /// <returns>
  /// A <see cref="Task"/> that represents the asynchronous save operation.
  /// The <see cref="Task"/> result contains the number of state entries written to the database.
  /// </returns>
  Task<int> SaveChangesAsync();

  /// <summary>
  /// Begins tracking the given <see cref="LinearizedGraph"/>, and any other reachable entities that are not already being tracked,
  /// in the Added state such that they will be inserted into the database when SaveChanges() is called.
  /// </summary>
  /// <param name="graph">The <see cref="LinearizedGraph"/> to add.</param>
  /// <returns>
  /// The <see cref="EntityEntry{TEntity}"/>.
  /// The entry provides access to change tracking information and operations for the entity.
  /// </returns>
  EntityEntry<LinearizedGraph> AddGraph(LinearizedGraph graph);

  /// <summary>
  /// Begins tracking the given <see cref="LinearizedGraph"/> in the Deleted state such that it will be removed from the database when SaveChanges() is called.
  /// </summary>
  /// <param name="graph">The <see cref="LinearizedGraph"/> to remove.</param>
  /// <returns>
  /// The <see cref="EntityEntry{TEntity}"/>.
  /// The entry provides access to change tracking information and operations for the entity.
  /// </returns>
  EntityEntry<LinearizedGraph> RemoveGraph(LinearizedGraph graph);

  /// <summary>
  /// Checks if the <see cref="LinearizedGraph"/> with the given identification exists.
  /// </summary>
  /// <param name="id">The identification of the <see cref="LinearizedGraph"/>.</param>
  /// <returns>true if the <see cref="LinearizedGraph"/> exists, else false.</returns>
  bool LinearizedGraphExists(Guid id);
}