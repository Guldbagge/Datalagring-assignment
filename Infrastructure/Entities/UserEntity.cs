using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool AcceptsUserTerms { get; set; }
    public bool AcceptsMarketingTerms { get; set; }
    public int RoleId { get; set; }
    public virtual RoleEntity Role { get; set; } = null!;
    public virtual AuthEntity Authentication { get; set; } = null!;
}
