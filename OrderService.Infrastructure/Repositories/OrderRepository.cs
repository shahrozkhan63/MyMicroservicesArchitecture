using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderItems)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderNumber = o.OrderNumber,
                    OrderDate = o.OrderDate,
                    CustomerName = o.CustomerName,
                    OrderItems = o.OrderItems.Select(item => new OrderItemViewModel
                    {
                        OrderId = item.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        ProductName = item.ProductName,
                        ProductPrice = Convert.ToDecimal(item.ProductPrice)
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders.Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null) return null;

            return new OrderViewModel
            {
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                CustomerName = order.CustomerName,
                OrderItems = order.OrderItems.Select(item => new OrderItemViewModel
                {
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ProductName = item.ProductName,
                    ProductPrice = Convert.ToDecimal(item.ProductPrice)
                }).ToList()
            };
        }

        public async Task AddOrderAsync(OrderViewModel orderViewModel)
        {
            var order = new Order
            {
                OrderNumber = orderViewModel.OrderNumber,
                OrderDate = orderViewModel.OrderDate,
                CustomerName = orderViewModel.CustomerName,
                OrderItems = orderViewModel.OrderItems.Select(item => new OrderItem
                {
                    // Assuming OrderId should be set by EF, do not set it here.
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ProductName = item.ProductName,
                    ProductPrice = item.ProductPrice
                }).ToList() // Ensure this is a List<OrderItem>
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(OrderViewModel orderViewModel)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems) // Load existing OrderItems
                .FirstOrDefaultAsync(o => o.OrderId == orderViewModel.OrderId);

            if (order != null)
            {
                // Update the order properties
                order.OrderNumber = orderViewModel.OrderNumber;
                order.OrderDate = orderViewModel.OrderDate;
                order.CustomerName = orderViewModel.CustomerName;

                // Clear existing OrderItems and add new ones
                order.OrderItems.Clear();
                order.OrderItems.AddRange(orderViewModel.OrderItems.Select(item => new OrderItem
                {
                    // Assuming OrderId should not be set here for new items.
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ProductName = item.ProductName,
                    ProductPrice = item.ProductPrice
                }));

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }


}
