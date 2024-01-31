using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<RoleEntity> Roles { get; set; }
        public virtual DbSet<AuthEntity> Authentications { get; set; }
        public virtual DbSet<BankSwishDetailsEntity> BankSwishDetails { get; set; }
        public virtual DbSet<ReviewFeedbackEntity> ReviewFeedbacks { get; set; }
        public virtual DbSet<UserTypeEntity> UserTypes { get; set; }
        public virtual DbSet<OrderEntity> Order { get; set; }
        public virtual DbSet<OrderRowEntity> OrderRow { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<Product>();
            modelBuilder.Ignore<Category>();
        }
    }
}
