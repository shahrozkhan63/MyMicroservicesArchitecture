using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int orderId)
        {
            return await _context.Products.FirstOrDefaultAsync(o => o.ProductId == orderId);
        }

        public async Task AddProductAsync(Product order)
        {
            await _context.Products.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product order)
        {
            _context.Products.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int orderId)
        {
            var order = await _context.Products.FindAsync(orderId);
            if (order != null)
            {
                _context.Products.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
