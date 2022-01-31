using System;
using System.Collections.Generic;
using System.Linq;
using DALWithTests.DataAccessLayer.Entities;
using DALWithTests.DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DALWithTests.UnitTests
{
    [TestClass]
    public class OrdersTests
    {
        [TestMethod]
        public void GetAllOrdersTest()
        {
            OrdersRepository ordersRepository = new OrdersRepository();
            IEnumerable<Order> ordersList = ordersRepository.GetAll();
            Assert.AreNotEqual(0, ordersList.Count());
        }

        [TestMethod]
        public void GetOrder()
        {
            OrdersRepository ordersRepository = new OrdersRepository();
            Order order = ordersRepository.Get(10257);
            Assert.IsNotNull(order);
        }
        
        [TestMethod]
        public void GetOrderInformation()
        {
            OrdersRepository ordersRepository = new OrdersRepository();
            OrderInformation orderInformation = ordersRepository.GetOrderInformation(10257);
            Assert.AreEqual("HILAA", orderInformation.Order.CustomerID);
            Assert.AreEqual(3, orderInformation.Products.Count);
        }
        
        [TestMethod]
        public void CreateNewOrder()
        {
            OrdersRepository ordersRepository = new OrdersRepository();
            var ordersCount = ordersRepository.GetAll().Count();
            DateTime deliveryTime = DateTime.Now.AddDays(5);
            decimal freight = 26.75m;
            Order order = new Order("BONAP", 6, deliveryTime, 3, freight,
                "Ernst Handel", "Kirchgasse 6", "Graz", null, "8010", "Austria");
            ordersRepository.Create(order);
            Assert.AreEqual(ordersCount + 1, ordersRepository.GetAll().Count());
        }

        [TestMethod]
        public void SetNewOrderInProgress()
        {
            OrdersRepository ordersRepository = new OrdersRepository();
            DateTime deliveryTime = DateTime.Now.AddDays(5);
            decimal freight = 26.75m;
            Order order = new Order("BONAP", 6, deliveryTime, 3, freight,
                "Ernst Handel", "Kirchgasse 6", "Graz", null, "8010", "Austria");
            int orderID = ordersRepository.Create(order).OrderID;
            ordersRepository.SetInProgress(order);
            order = ordersRepository.Get(orderID);
            Assert.IsNotNull(order.OrderDate);
        }
        
        [TestMethod]
        public void UpdateNewOrder()
        {
            string newPostalCode = "1488";
            OrdersRepository ordersRepository = new OrdersRepository();
            DateTime deliveryTime = DateTime.Now.AddDays(5);
            decimal freight = 26.75m;
            Order order = new Order("BONAP", 6, deliveryTime, 3, freight,
                "Ernst Handel", "Kirchgasse 6", "Graz", null, "8010", "Austria");
            int orderID = ordersRepository.Create(order).OrderID;
            order.ShipPostalCode = newPostalCode;
            ordersRepository.Update(order);
            order = ordersRepository.Get(orderID);
            Assert.AreEqual(newPostalCode, order.ShipPostalCode);
        }
    }
}
