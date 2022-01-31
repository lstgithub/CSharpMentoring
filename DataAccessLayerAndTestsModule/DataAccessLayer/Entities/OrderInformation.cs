using System;
using System.Collections.Generic;
using System.Text;

namespace HT6.Entities
{
    public class OrderInformation
    {
        public Order Order { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public List<Product> Products { get; set; }

        public OrderInformation(Order order, OrderDetails details, List<Product> products)
        {
            Order = order;
            OrderDetails = details;
            Products = products;
        }
    }
}
