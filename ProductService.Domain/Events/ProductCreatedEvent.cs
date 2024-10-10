using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Events
{
    public class ProductCreatedEvent
    {
        public int ProductId { get; set; }          // Unique identifier for the product
        public string Name { get; set; }             // Name of the product
        public string Description { get; set; }      // Description of the product
        public decimal Price { get; set; } = 0;
    }
}
