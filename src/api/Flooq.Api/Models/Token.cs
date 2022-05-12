using System.ComponentModel.DataAnnotations;

namespace Flooq.Api.Models
{
  public class Token
    {
        [Key]
        public Guid? Id { get; set;}
        public string? Name { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? LastEdited { get; set; }
        [Encrypted]
        public string? Value { get; set; }
    }
}