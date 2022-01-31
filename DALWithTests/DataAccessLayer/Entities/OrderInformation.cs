using System.Collections.Generic;

namespace DALWithTests.DataAccessLayer.Entities
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
