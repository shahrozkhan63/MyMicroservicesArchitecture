using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int ProductId);
        Task AddProductAsync(Product Product);
        Task UpdateProductAsync(Product Product);
        Task DeleteProductAsync(int ProductId);
    }
}
