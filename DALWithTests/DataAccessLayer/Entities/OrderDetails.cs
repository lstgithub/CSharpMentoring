using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace HT6.Entities
{
    public class OrderDetails
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public SqlMoney UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Single Discount { get; set; }
    }
}
