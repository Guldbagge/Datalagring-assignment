using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class AuthEntity
{
    [Key]
    public int UserId { get; set; }
    public string Password { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public string? AccessToken { get; set; }
    public DateTime LastSignedIn { get; set; } = DateTime.Now;
    public bool IsPresistent { get; set; }
    public virtual UserEntity User { get; set; } = null!;
}