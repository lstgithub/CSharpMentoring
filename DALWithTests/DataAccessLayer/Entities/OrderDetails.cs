using System;
using System.Data.SqlTypes;

namespace DALWithTests.DataAccessLayer.Entities
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
