using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces;

public interface IOrderRepository : IRepo<OrderEntity>
{
    Task<OrderEntity> CreateAsync(OrderEntity entity, string callingMethod = "");
    Task<OrderEntity> GetAsync(Expression<Func<OrderEntity, bool>> predicate);
    Task<OrderEntity> UpdateAsync(Expression<Func<OrderEntity, bool>> predicate, OrderEntity entity);
    Task<bool> DeleteAsync(Expression<Func<OrderEntity, bool>> predicate);
}
