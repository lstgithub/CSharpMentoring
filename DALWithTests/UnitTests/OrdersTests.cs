using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HT6.Entities;
using HT6.Repositories;

namespace HT6Tests
{
    [TestClass]
    public class OrdersTests
    {
        [TestMethod]
        public void GetAllOrdersTest()
        {
            //Arrange
            OrdersRepository ordersRepository = new OrdersRepository();

            //Act
            IEnumerable<Order> ordersList = ordersRepository.GetAll();

            //Assert
            Assert.AreNotEqual(0, ordersList.Count());
        }

        [TestMethod]
        public void GetOrder()
        {
            //Arrange
            OrdersRepository ordersRepository = new OrdersRepository();

            //Act
            Order order = ordersRepository.Get(10257);

            //Assert
            Assert.IsNotNull(order);
        }
        
        [TestMethod]
        public void GetOrderInformation()
        {
            //Arrange
            OrdersRepository ordersRepository = new OrdersRepository();

            //Act
            OrderInformation orderInformation = ordersRepository.GetOrderInformation(10257);

            //Assert
            Assert.AreEqual("HILAA", orderInformation.Order.CustomerID);
            Assert.AreEqual(3, orderInformation.Products.Count);
        }
        
        [TestMethod]
        public void CreateNewOrder()
        {
            //Arrange
            OrdersRepository ordersRepository = new OrdersRepository();
            var ordersCount = ordersRepository.GetAll().Count();
            DateTime deliveryTime = DateTime.Now.AddDays(5);
            decimal freight = 26.75m;
            Order order = new Order("BONAP", 6, deliveryTime, 3, freight,
                "Ernst Handel", "Kirchgasse 6", "Graz", null, "8010", "Austria");

            //Act
            ordersRepository.Create(order);

            //Assert
            Assert.AreEqual(ordersCount + 1, ordersRepository.GetAll().Count());
        }

        [TestMethod]
        public void SetNewOrderInProgress()
        {
            //Arrange
            OrdersRepository ordersRepository = new OrdersRepository();
            DateTime deliveryTime = DateTime.Now.AddDays(5);
            decimal freight = 26.75m;
            Order order = new Order("BONAP", 6, deliveryTime, 3, freight,
                "Ernst Handel", "Kirchgasse 6", "Graz", null, "8010", "Austria");

            //Act
            int orderID = ordersRepository.Create(order).OrderID;
            ordersRepository.SetInProgress(order);
            order = ordersRepository.Get(orderID);

            //Assert
            Assert.IsNotNull(order.OrderDate);
        }
        
        [TestMethod]
        public void UpdateNewOrder()
        {
            //Arrange 
            string newPostalCode = "1488";

            //Act
            OrdersRepository ordersRepository = new OrdersRepository();
            DateTime deliveryTime = DateTime.Now.AddDays(5);
            decimal freight = 26.75m;
            Order order = new Order("BONAP", 6, deliveryTime, 3, freight,
                "Ernst Handel", "Kirchgasse 6", "Graz", null, "8010", "Austria");
            int orderID = ordersRepository.Create(order).OrderID;
            order.ShipPostalCode = newPostalCode;
            ordersRepository.Update(order);
            order = ordersRepository.Get(orderID);

            //Assert
            Assert.AreEqual(newPostalCode, order.ShipPostalCode);
        }
    }
}
