using OrderService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
    }
}
