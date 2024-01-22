using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public interface IUserRepository : IRepo<UserEntity>
{
    Task<List<UserEntity>> GetAllAsync();
}
