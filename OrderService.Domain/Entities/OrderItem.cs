using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }  // No direct reference to Product entity

        public int Quantity { get; set; }

        // Optionally, store some additional product details
        public string ProductName { get; set; }    // Cached product name
        public decimal ProductPrice { get; set; }  // Cached product price
    }


}
