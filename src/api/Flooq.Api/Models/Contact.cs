using System.ComponentModel.DataAnnotations;

namespace Flooq.Api.Models;

public class Contact
{
  [Key]
  public string Email { get; set; }
}