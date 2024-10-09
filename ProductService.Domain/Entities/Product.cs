using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }          // Unique identifier for the product
        public string Name { get; set; }             // Name of the product
        public string Description { get; set; }      // Description of the product
        public decimal Price { get; set; }           // Price of the product

        // Navigation property (if needed) for any related entities within the Product service
        // For example, you might have categories, tags, or inventory tracking
        // public ICollection<ProductCategory> Categories { get; set; }
    }

}
