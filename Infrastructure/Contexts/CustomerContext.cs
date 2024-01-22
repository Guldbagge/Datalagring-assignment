using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class CustomerContext(DbContextOptions<CustomerContext> options) : DbContext(options)
{
    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<RoleEntity> Roles { get; set; }
    public virtual DbSet<AuthEntity> Authentications { get; set; }
}
