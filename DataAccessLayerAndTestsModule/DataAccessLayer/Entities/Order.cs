using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace HT6.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime ? OrderDate { get; set; }
        public DateTime ? RequiredDate { get; set; }
        public DateTime ? ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public Statuses Status { get; private set; }

        public Order()
        {
        }

        public Order(int orderId, string customerId,
            int employeeId, DateTime ? orderDate,
            DateTime requiredDate, DateTime ? shippedDate, int shipVia,
            decimal freight, string shipName,
            string shipAddress, string shipCity,
            string shipRegion, string shipPostalCode, string shipCountry)
        {
            OrderID = orderId;
            CustomerID = customerId;
            EmployeeID = employeeId;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            ShipVia = shipVia;
            Freight = freight;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipRegion = shipRegion;
            ShipPostalCode = shipPostalCode;
            ShipCountry = shipCountry;
            if (OrderDate == null)
            {
                Status = Statuses.New;
            }
            if (ShippedDate == null)
            {
                Status = Statuses.InProgress;
            }
            if (ShippedDate != null)
            {
                Status = Statuses.Completed;
            }
        }

        public Order(string customerId,
            int employeeId,
            DateTime requiredDate, int shipVia,
            decimal freight, string shipName,
            string shipAddress, string shipCity,
            string shipRegion, string shipPostalCode, string shipCountry)
        {
            CustomerID = customerId;
            EmployeeID = employeeId;
            OrderDate = null;
            RequiredDate = requiredDate;
            ShippedDate = null;
            ShipVia = shipVia;
            Freight = freight;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipRegion = shipRegion;
            ShipPostalCode = shipPostalCode;
            ShipCountry = shipCountry;
            if (OrderDate == null)
            {
                Status = Statuses.New;
            }
            else if (ShippedDate == null)
            {
                Status = Statuses.InProgress;
            }
            if (ShippedDate != null)
            {
                Status = Statuses.Completed;
            }
        }

        public void ChangeStatus(Statuses status)
        {
            Status = status;
        }
    }

    public enum Statuses
    {
        New,
        InProgress,
        Completed
    }
}
