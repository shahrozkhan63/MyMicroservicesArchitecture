using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public string? OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; } = null!;

        public virtual ICollection<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    }

}
