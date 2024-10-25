
using OrderService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();
        Task<OrderViewModel> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(OrderViewModel order);
        Task UpdateOrderAsync(OrderViewModel order);
        Task DeleteOrderAsync(int orderId);
    }
}
